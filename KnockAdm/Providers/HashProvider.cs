using System;
using System.Security.Cryptography;
using System.Text;

namespace KnockAdm
{
	public class HashProvider
	{
		private string salt = "lkjfsd8908fu9p5aoekvfjsd94wpochf";

		public string GetPasswordHash(string password)
		{
			if (String.IsNullOrEmpty(password))
			{
				return String.Empty;
			}

			StringBuilder sb = new StringBuilder();
			using (SHA1 sha = new SHA1CryptoServiceProvider())
			{
				foreach (byte b in sha.ComputeHash(Encoding.UTF8.GetBytes(salt + password)))
				{
					sb.Append(b.ToString("X2"));
				}
			}
			return sb.ToString();
		}
	}
}
