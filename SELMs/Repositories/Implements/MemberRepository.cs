using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Dapper;
using SELMs.Models;
using SELMs.Models.BusinessModel;

namespace SELMs.Repositories.Implements
{
	public class MemberRepository : IMemberRepository
	{
		private SELMsContext db = new SELMsContext();

		public dynamic DeleteMember(User member)
		{
			throw new NotImplementedException();
		}

		public dynamic GetMember(int id)
		{
			dynamic member = db.Users.Where(u => u.user_id == id).FirstOrDefaultAsync();
			return member;
		}


		public dynamic GetMemberList(Argument arg)
		{
			dynamic members = null;
			members = db.Database.Connection.QueryAsync<dynamic>("Proc_GetMembersList", new
			{
				username = arg.username,
				isadmin = arg.isadmin,
				role = arg.role,
				text = arg.text
			}
				, commandType: CommandType.StoredProcedure);
			return members;
		}

		public dynamic SaveMember(User member)
		{
			db.Users.Add(member);
			db.SaveChangesAsync();
			return member;
		}

		public void UpdateMember(User member)
		{
			User orgMember = db.Users.Where(u => u.user_id == member.user_id).FirstOrDefault();
			db.Entry(orgMember).CurrentValues.SetValues(member);
			db.SaveChangesAsync();
		}

		public string GetLastMemberCode(string prefix)
		{
			User obj = db.Users.Where(u => u.user_code.StartsWith(prefix)).OrderByDescending(u => u.user_code).FirstOrDefault();
			if (obj != null)
			{
				string result = obj.user_code == null ? "" : obj.user_code;
				return result;
			}
			return "";
		}

		public dynamic GetRoleList()
		{
			dynamic members = null;
			members = db.Database.Connection.QueryAsync<dynamic>("Proc_GetRolesList"
				, commandType: CommandType.StoredProcedure);
			return members;
		}

		public async Task SendGmailAllocationAccountAsync(User mem)
		{
			try
			{
				string senderEmail = "lymthe153498@fpt.edu.vn";
				string senderPassword = "vhfg myqq lxec uakt";
				string recipientEmail = mem.email;
				string subject = "Email allocation account from SELMS";
				string body = "Hello, this is account SELMS for you: Account: " + mem.username + "; Password: " + mem.password;


				using (var client = new SmtpClient("smtp.gmail.com"))
				{
					client.Port = 587;
					client.UseDefaultCredentials = false;
					client.Credentials = new NetworkCredential(senderEmail, senderPassword);
					client.EnableSsl = true;

					var message = new MailMessage(senderEmail, recipientEmail);
					message.Subject = subject;
					message.Body = body;

					await client.SendMailAsync(message);

					Console.WriteLine("Email has been sent successfully!");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
				throw; // Re-throw the exception after logging or handling
			}
		}

		public async Task<User> GetMemberByCodeOrUserName(string usernameOrCode)
		{
			return await db.Users.FirstOrDefaultAsync(u => u.username.Equals(usernameOrCode) || u.user_code.Equals(usernameOrCode));
		}
	}



}