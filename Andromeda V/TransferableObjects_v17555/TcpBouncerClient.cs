using System;
using System.Net;
using System.Net.Sockets;

public class TcpBouncerClient
{
	private TcpClient c;

	private NetworkStream ns;

	public TcpBouncerClient()
	{
		IPEndPoint pEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 13700);
		this.c = new TcpClient()
		{
			NoDelay = true
		};
		this.c.Connect(pEndPoint);
		this.ns = this.c.GetStream();
	}
}