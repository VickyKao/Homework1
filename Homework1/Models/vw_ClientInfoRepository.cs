using System;
using System.Linq;
using System.Collections.Generic;
	
namespace Homework1.Models
{   
	public  class vw_ClientInfoRepository : EFRepository<vw_ClientInfo>, Ivw_ClientInfoRepository
	{

	}

	public  interface Ivw_ClientInfoRepository : IRepository<vw_ClientInfo>
	{

	}
}