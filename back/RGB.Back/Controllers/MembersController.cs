using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RGB.Back.Models;
using RGB.Back.DTOs;
using RGB.Back.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.CodeAnalysis;
using Microsoft.IdentityModel.Tokens;
using RGB.Back.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using RGB.Back.Infra;
using System.Runtime.Intrinsics.X86;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;



namespace RGB.Back.Controllers
{
    [EnableCors("AllowAny")]
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly RizzContext _context;
		private readonly MemberService _service;
		private readonly IDataProtector _dataProtector;
		private readonly JwtConfig _jwtConfig;
		private readonly JWTservice _jwtservice;


		public MembersController(IOptionsMonitor<JwtConfig> optionsMonitor,RizzContext context)
        {
			_context = context;
			_service = new MemberService(context);
			_jwtConfig = optionsMonitor.CurrentValue;
			_jwtservice = new JWTservice("private_key.xml", "public_key.xml");

			// 创建服务集合
			var serviceCollection = new ServiceCollection();
			// 向服务集合中添加数据保护服务
			serviceCollection.AddDataProtection();
			// 构建服务提供程序
			var serviceProvider = serviceCollection.BuildServiceProvider();
			// 从服务提供程序中获取数据保护服务
			var dataProtectionProvider = serviceProvider.GetRequiredService<IDataProtectionProvider>();
			// 建立保護器 
			_dataProtector = dataProtectionProvider.CreateProtector("SamplePurpose");
		}

        [HttpPost("Login")]
		//LoginDTO loginDto
		//JwtPayload
		public async Task<List<string>> Login(LoginDTO loginDto)
		{
            var result= _service.ValidLogin(loginDto); // 驗證帳密是否ok,且是有效的會員
            var memberId = result.Item3.Id.ToString();
			var ava = result.Item3.AvatarUrl;
			var bouns = result.Item3.Bonus.ToString();
			var name = result.Item3.NickName;

			if (result.Item1 == false )
			{
				if(result.Item2 == 0)
				{
					string errorMessage = "帳號或密碼錯誤";
					List<string> errors = new List<string>();
					errors.Add(errorMessage);
					return errors;
				}
				else
				{
					//您尚未開通會員資格, 請先收確認信, 並點選信裡的連結, 完成認證, 才能登入本網站
					string errorMessage = "您尚未完成認證";
					List<string> errors = new List<string>();
					errors.Add(errorMessage);
					return errors;
				}
			}
			else
			{
				Claim idclaim = new Claim("memberid", memberId);
				Claim nameclaim = new Claim("membername", name);
				Claim avaclaim = new Claim("memberava", ava);
				Claim bounsclaim = new Claim("memberbouns", bouns);

				//jwt
				string jwt = _jwtservice.GenerateJWT(idclaim,nameclaim, avaclaim,bounsclaim);
				//
				string sussceMessage = "登入成功";
				List<string> sussce = new List<string>();
				var protectId = _dataProtector.Protect(memberId);
				sussce.Add(sussceMessage);
				// 保護後的id 
				sussce.Add(protectId);
				//無防禦的資料
				sussce.Add(ava);
				sussce.Add(bouns);
				sussce.Add(name);
				sussce.Add(memberId);
				//jwt
				sussce.Add(jwt);
				//
				//ProcessLogin(memberId);
				return sussce;
			}
		}

		[HttpPost("unjwt")]
		public string unjwt(string jwtToken)
		{
			var claims = _jwtservice.DecodeAndSerializeClaims(jwtToken);
			return claims;
		}




		[HttpPost("MemberId")]
		public async Task<string> MemberId(string protectId)
        {
			var Id = _dataProtector.Unprotect(protectId);
			return Id;
		}

		[HttpPost("TestMemberAccount")]
		public async Task<bool> TestMemberAccount(string account)
		{
			var member = _context.Members.FirstOrDefault(p => p.Account == account);
			if(member == null)
			{
			   return true;
			}
			else
			{
				return false;
			}

		}

		[HttpPost("TestMemberName")]
		public async Task<bool> TestMemberName(string name)
		{
			var member = _context.Members.FirstOrDefault(p => p.NickName == name);
			if (member == null)
			{
				return true;
			}
			else
			{
				return false;
			}
		}



		// PUT: api/Members/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpGet("{id}")]
		public async Task<CheckMemberDTO> GetMemberData(string id)
		{
			var unprotectId = _dataProtector.Unprotect(id);
			var member = _context.Members.Find(Convert.ToInt32(unprotectId));
			string originalDateTime = member.RegistrationDate.ToString();
			DateTime dateTime = DateTime.Parse(originalDateTime);

			string formattedDate = dateTime.ToString("yyyy-MM-dd");

			var dto = new CheckMemberDTO
			{
				RegistrationDate = formattedDate,
				NickName = member.NickName
			};
			//         return "編輯成功";
			return dto;
		}


		//免密碼信發送
		[HttpPost("noPassword")]
		public async Task<string> NoPassword(string account)
		{

			var member = _context.Members.FirstOrDefault(m => m.Account == account);
			string Message = "已向您的郵箱發送信件,請查看郵箱完成登入";
			if(member == null)
			{
				Message = "帳號錯誤";

			}
			else
			{
				var confirmCode = Guid.NewGuid().ToString("N");
				member.ConfirmCode = confirmCode;
				_context.SaveChanges();
				var urlTemplate = "http" + "://" +  // 生成 http:.// 或 https://
	"localhost:3000" + "/" + // 生成網域名稱或 ip
	"Id={0}" + "/" +
	"LoginconfirmCode={1}";
				var url = string.Format(urlTemplate, member.Id, member.ConfirmCode);
				string name = account; // 請確認您的 CreateMemberDTO 類中是否包含了名稱（Name）和電子郵件（EMail）屬性
				string email = member.Mail;
				new EMailHelper().SendNoPasswordLoginEmail(url, name, email);
			}


			return Message;
		}



		//免密碼登入
		[HttpPost("noPasswordLogin")]
		public async Task<List<string>> NoPasswordLogin(ActiveDTO dto)
		{

			var member = _context.Members.FirstOrDefault(m=>m.Id== Convert.ToInt32(dto.Id) && m.ConfirmCode ==dto.confirmCode);
			string sussceMessage = "登入成功";
			if (member == null)
			{
				List<string> message = new List<string>();
				message.Add("連結已失效");
				return message;
			}
			else
			{
				member.ConfirmCode = null; // 清空 confirm code 欄位
				_context.SaveChanges();
			};

			
			List<string> sussce = new List<string>();

			var protectId = _dataProtector.Protect(member.Id.ToString());
			sussce.Add(sussceMessage);
			sussce.Add(protectId);
			sussce.Add(member.AvatarUrl);
			sussce.Add(member.Bonus.ToString());
			sussce.Add(member.NickName);
			sussce.Add(member.Id.ToString());

			return sussce;
		}

		//修改頭像
		[HttpPut("ava{id}")]
		public async Task<string> EditMemberAVA(string id, string ava)
		{
			var unprotectId = _dataProtector.Unprotect(id);

			var member = _context.Members.Find(Convert.ToInt32(unprotectId));

			member.AvatarUrl = ava;

			_context.SaveChanges();

			//         return "編輯成功";
			return "編輯成功";
		}

		//修改
		[HttpPut("{id}")]
		public async Task<string> EditMember(string id , string nickName)
        {
			var unprotectId = _dataProtector.Unprotect( id);

			var member = _context.Members.Find(Convert.ToInt32(unprotectId));

			member.NickName = nickName;

			_context.SaveChanges();

   //         return "編輯成功";
              return member.NickName;
        }

        // POST: api/Members
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //新增
        [HttpPost("Create")]
        public async Task<string> CreateMember(CreateMemberDTO cmDto)
        {
			//var urlTemplate = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/Members";
			var result= _service.CreateMember(cmDto);

            return result;
        }

        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.Id == id);
        }

        //      //將使用者資訊存入cookie
        private void ProcessLogin(string memberId)
        {
            CookieOptions options = new CookieOptions();
            // 设置过期时间
            options.Expires = DateTime.Now.AddHours(1);
			//對程式
			var protectText = _dataProtector.Protect(memberId);
			var unprotectText = _dataProtector.Unprotect(protectText);
			HttpContext.Response.Cookies.Append("Account", protectText, options);
            HttpContext.Response.Cookies.Append("AccountUN", unprotectText, options);
        }

		//vue傳入Id google登入
		[HttpPost("GoogleId")]
		public async Task<List<string>> ValidGoogleId(string googleId)
		{
			Member member =  _context.Members.FirstOrDefault(m => m.Google == googleId);
			if (member == null) {
				List<string> errors = new List<string>();
				errors.Add("新的google帳號");
				return errors;
			}
			else
			{
			    var memberId = member.Id.ToString();
				var ava = member.AvatarUrl;
				var bouns = member.Bonus.ToString();
				var name = member.NickName;
				if (ava == null)
				{
					//預設頭像
					ava = "";
				}
				string sussceMessage = "登入成功";
				List<string> sussces = new List<string>();
				var protectId = _dataProtector.Protect(memberId);
				sussces.Add(sussceMessage);
				sussces.Add(protectId);
				sussces.Add(ava);
				sussces.Add(bouns);
				sussces.Add(name);
				return sussces;
			}
		}


		//確認郵箱
		[HttpPost("ActiveRegister")]
		public async Task<int> ActiveRegister(ActiveDTO dto)
		{
			var result = _service.ActiveRegister(Convert.ToInt32(dto.Id), dto.confirmCode);
			
			if (result == false)
			{
				//驗證失敗
				return 0;
			}
			else
			{
				//已通過驗證
				return 1;
			}
		}


		/// <summary>
		/// 產生JWT Token
		/// </summary>
		/// <param name="user">User資料</param>
		/// <returns>AuthResult</returns>
		private async Task<AuthResult> GenerateJwtToken(string memberid)
		{
			#region 建立JWT Token
			//宣告JwtSecurityTokenHandler，用來建立token
			JwtSecurityTokenHandler jwtTokenHandler = new JwtSecurityTokenHandler();

			//appsettings中JwtConfig的Secret值
			byte[] key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

			//定義token描述
			SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
			{
				//設定要加入到 JWT Token 中的聲明資訊(Claims)
				Subject = new ClaimsIdentity(new[]
				{
		           new Claim(JwtRegisteredClaimNames.Iss, "RGB"),
				   new Claim("memberid", memberid)
				}),

				//設定Token的時效
				Expires = DateTime.UtcNow.AddSeconds(3600),

				//設定加密方式，key(appsettings中JwtConfig的Secret值)與HMAC SHA256演算法
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			//使用SecurityTokenDescriptor建立JWT securityToken
			SecurityToken token = jwtTokenHandler.CreateToken(tokenDescriptor);


			//token序列化為字串
			string jwtToken = jwtTokenHandler.WriteToken(token);
			#endregion

			//#region 回傳AuthResult
			return new AuthResult()
			{
				Token = jwtToken,
				Result = true,
				RefreshToken = RandomString(25) + Guid.NewGuid()
			};
			//#endregion
		}

		public static string RandomString(int length)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			var random = new Random();
			var result = new StringBuilder(length);
			for (int i = 0; i < length; i++)
			{
				result.Append(chars[random.Next(chars.Length)]);
			}
			return result.ToString();
		}

		[HttpGet("en")]
		public async Task<string> test(string word)
		{



			return _service.test(word);


		}

		[HttpGet("un")]
		public async Task<string> test2(string word)
		{

			return _service.test2(word);

		}

	}
}
