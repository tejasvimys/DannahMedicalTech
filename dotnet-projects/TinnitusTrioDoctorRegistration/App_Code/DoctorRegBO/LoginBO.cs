using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LoginBO
/// </summary>
public class LoginBO
{
	public LoginBO()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string UserName { get; set; }
    public string Password { get; set; }

    public string Pin { get; set; }
    public bool IsFirstLogin { get; set; }
}