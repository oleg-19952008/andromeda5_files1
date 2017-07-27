using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //   Response.Write(Encoding.UTF8.GetString(Convert.FromBase64String("PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0idXRmLTgiPz4NCjxtYWluPjxnZW9pcF9sb2dpbl9zZXJ2ZXI+MTI3LjAuMC4xPC9nZW9pcF9sb2dpbl9zZXJ2ZXI+PGdlb2lwX2Fzc2V0X3NlcnZlcj5odHRwOi8vMTI3LjAuMC4xL2Fzc2V0czwvZ2VvaXBfYXNzZXRfc2VydmVyPjxzZXJ2ZXJzPjxzZXJ2ZXI+PHNlcnZlcl9uYW1lPkVVIEdsb2JhbDwvc2VydmVyX25hbWU+PGxvZ2luX3NlcnZlcj4xMjcuMC4wLjE8L2xvZ2luX3NlcnZlcj48YXNzZXRfc2VydmVyPmh0dHA6Ly8xMjcuMC4wLjEvYXNzZXRzPC9hc3NldF9zZXJ2ZXI+PGdhbWVfZG9tYWluPjEyNy4wLjAuMTwvZ2FtZV9kb21haW4+PGdhbWVfdHlwZT5pbnQ8L2dhbWVfdHlwZT48d29ybGRfaWQ+MTwvd29ybGRfaWQ+PHVybF9xdWl0Pmh0dHA6Ly9nYW1lLjEyNy4wLjAuMS9wbGF5LnBocDwvdXJsX3F1aXQ+PHVybF9yZWdpc3Rlcj5odHRwOi8vMTI3LjAuMC4xLz9zaG93PXJlZ2lzdHJ5PC91cmxfcmVnaXN0ZXI+PHVybF9mb3Jnb3R0ZW4+aHR0cDovLzEyNy4wLjAuMS8/c2hvdz1mb3Jnb3R0ZW48L3VybF9mb3Jnb3R0ZW4+PHVybF9hdmF0YXJzPmh0dHA6Ly9nYW1lLjEyNy4wLjAuMS9hdmF0YXJzL2ludC93b3JsZF8xL3swfS5qcGc8L3VybF9hdmF0YXJzPjx1cmxfcGF5bWVudD5odHRwOi8vZ2FtZS4xMjcuMC4wLjEvcGxheS5waHA/c2hvdz1wYXltZW50czwvdXJsX3BheW1lbnQ+PGhhdmVfYWNjb3VudD4wPC9oYXZlX2FjY291bnQ+PGNvbnRpbmVudD5ldXJvcGU8L2NvbnRpbmVudD48L3NlcnZlcj48L3NlcnZlcnM+PC9tYWluPg==")));
        var s = "PG1haW4+PGdlb2lwX2xvZ2luX3NlcnZlcj4xMjcuMC4wLjE8L2dlb2lwX2xvZ2luX3NlcnZlcj48Z2VvaXBfYXNzZXRfc2VydmVyPmh0dHA6Ly8xMjcuMC4wLjEvYXNzZXRzPC9nZW9pcF9hc3NldF9zZXJ2ZXI+PHNlcnZlcnM+PHNlcnZlcj48c2VydmVyX25hbWU+RVUgR2xvYmFsPC9zZXJ2ZXJfbmFtZT48bG9naW5fc2VydmVyPjEyNy4wLjAuMTwvbG9naW5fc2VydmVyPjxhc3NldF9zZXJ2ZXI+aHR0cDovLzEyNy4wLjAuMS9hc3NldHM8L2Fzc2V0X3NlcnZlcj48Z2FtZV9kb21haW4+MTI3LjAuMC4xPC9nYW1lX2RvbWFpbj48Z2FtZV90eXBlPmludDwvZ2FtZV90eXBlPjx3b3JsZF9pZD4xPC93b3JsZF9pZD48dXJsX3F1aXQ+aHR0cDovLzEyNy4wLjAuMS9wbGF5LnBocDwvdXJsX3F1aXQ+PHVybF9yZWdpc3Rlcj5odHRwOi8vMTI3LjAuMC4xLz9zaG93PXJlZ2lzdHJ5PC91cmxfcmVnaXN0ZXI+PHVybF9mb3Jnb3R0ZW4+aHR0cDovLzEyNy4wLjAuMS8/c2hvdz1mb3Jnb3R0ZW48L3VybF9mb3Jnb3R0ZW4+PHVybF9hdmF0YXJzPmh0dHA6Ly8xMjcuMC4wLjEvYXZhdGFycy9pbnQvd29ybGRfMS97MH0uanBnPC91cmxfYXZhdGFycz48dXJsX3BheW1lbnQ+aHR0cDovLzEyNy4wLjAuMS9wbGF5LnBocD9zaG93PXBheW1lbnRzPC91cmxfcGF5bWVudD48aGF2ZV9hY2NvdW50PjE8L2hhdmVfYWNjb3VudD48Y29udGluZW50PmV1cm9wZTwvY29udGluZW50Pjwvc2VydmVyPjwvc2VydmVycz48L21haW4+";
        Response.Write(Encoding.UTF8.GetString(Convert.FromBase64String(s)));
    }
}
/*
<? xml version="1.0" encoding="utf-8"?>
<main><geoip_login_server>127.0.0.1
</geoip_login_server><geoip_asset_server>http://127.0.0.1/assets
</geoip_asset_server><servers><server><server_name>EU Global
</server_name><login_server>127.0.0.1
</login_server><asset_server>http://127.0.0.1/assets
</asset_server><game_domain>127.0.0.1
</game_domain><game_type>int
</game_type><world_id>1
</world_id><url_quit>http://game.127.0.0.1/play.php
</url_quit><url_register>http://127.0.0.1/?show=registry
</url_register><url_forgotten>http://127.0.0.1/?show=forgotten
</url_forgotten><url_avatars>http://game.127.0.0.1/avatars/int/world_1/{0}.jpg
</url_avatars><url_payment>http://game.127.0.0.1/play.php?show=payments
</url_payment><have_account>0
</have_account><continent>europe
</continent>
</server>
</servers>
</main>

<?xml version="1.0" encoding="utf-8"?>
<main><geoip_login_server>217.174.159.11</geoip_login_server><geoip_asset_server>http://asset.andromeda5.com/assets</geoip_asset_server></main>

*/
