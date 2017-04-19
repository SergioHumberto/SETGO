using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IBatisNet.DataMapper;
using WebApplicationTemplate.Objects;

namespace WebApplicationTemplate.DAL
{
	public static class PaypalConfigDAL
	{
		public static PaypalConfigOBJ SelectActivePaypalConfigURL()
		{
			return Mapper.Instance().QueryForObject<PaypalConfigOBJ>("SelectActivePaypalConfigURL", null);
		}
	}
}
