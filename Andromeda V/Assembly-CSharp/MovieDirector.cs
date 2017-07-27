using System;
using UnityEngine;

public class MovieDirector : MonoBehaviour
{
	private int executeIndex;

	private float startTime;

	private Action updateAction;

	private MovieEvent[] _events;

	public MovieDirector()
	{
	}

	private void Start()
	{
	}

	public void StartMovie(MovieEvent[] events)
	{
		this.startTime = Time.get_time();
		this.executeIndex = 0;
		this._events = events;
		this.updateAction = new Action(this, MovieDirector.UpdateRunMovie);
	}

	public void StopMovie()
	{
		this.updateAction = null;
	}

	private void Update()
	{
		if (this.updateAction != null)
		{
			this.updateAction.Invoke();
		}
	}

	private void UpdateRunMovie()
	{
		if (this._events[this.executeIndex] == null)
		{
			MovieDirector movieDirector = this;
			movieDirector.executeIndex = movieDirector.executeIndex + 1;
			return;
		}
		float _time = Time.get_time() - this.startTime;
		while (this._events[this.executeIndex] != null && this._events[this.executeIndex].time <= _time)
		{
			this._events[this.executeIndex].Execute();
			MovieDirector movieDirector1 = this;
			movieDirector1.executeIndex = movieDirector1.executeIndex + 1;
			if (this.executeIndex < (int)this._events.Length)
			{
				continue;
			}
			this.StopMovie();
			return;
		}
	}
}