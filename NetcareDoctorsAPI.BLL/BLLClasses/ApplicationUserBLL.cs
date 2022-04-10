using NetcareDoctorsAPI.BLL.DataContract;
using NetcareDoctorsAPI.Data;
using NetcareDoctorsAPI.Data.DALClasses;
using NetcareDoctorsAPI.Data.Entities;

namespace NetcareDoctorsAPI.BLL.BLLClasses
{
    public class ApplicationUserBLL : SharedBLL
    {
        private readonly ApplicationUserDAL ApplicationUserDAL;

        public ApplicationUserBLL()
        {
            ApplicationUserDAL = new();
        }

        public async Task<AuthenticateResp> Authenticate()
        {
            using NetcareDoctorsContext _dbContext = new();

            AccessToken _accessToken = new()
            {
                AccessTokenValue = CreateAccessToken(),
                ExpiryDate = DateTime.Now.AddMonths(2).Date,
            };

            await _dbContext.AddAsync(_accessToken);
            await _dbContext.SaveChangesAsync();

            return FillAuthenticateResp(_accessToken);
        }

        public async Task<bool> IsAdministratorApplicationUser(string username)
        {
            using NetcareDoctorsContext _dbContext = new();

            return await ApplicationUserDAL.IsAdministratorUsernameExisting(_dbContext, username);
        }

        public async Task SignUp(string username, string userPassword)
        {
            using NetcareDoctorsContext _dbContext = new();

            if (await ApplicationUserDAL.IsUsernameExisting(_dbContext, username))
            {
                RaiseServerError("Username already existing");
            }

            ApplicationUser _applicationUser = new()
            {
                ApplicationUserId = Guid.NewGuid(),
                Username = username,
                UserPassword = userPassword,
                ApplicationUserType = FirmamentUtilities.Utilities.GetEnumDescription(NetcareDoctorsAPIEnum.ApplicationUserType.GeneralUser)
            };

            await _dbContext.AddAsync(_applicationUser);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<SignInResp> SignIn(string username, string userPassword)
        {
            using NetcareDoctorsContext _dbContext = new();

            ApplicationUser? _applicationUser = await ApplicationUserDAL.GetApplicationUserByUsernameAndUserPassword(_dbContext, username, userPassword);

            if (_applicationUser is null)
            {
                RaiseServerError("Invalid Username or Password");
            }

            if (!string.Equals(_applicationUser.Username, username, StringComparison.CurrentCulture))
            {
                RaiseServerError("Invalid Username or Password");
            }

            if (!string.Equals(_applicationUser.UserPassword, userPassword, StringComparison.CurrentCulture))
            {
                RaiseServerError("Invalid Username or Password");
            }

            return FillSignInResp(_applicationUser);
        }

        public async Task<bool> IsAccessTokenValid(string accessToken)
        {
            using NetcareDoctorsContext _dbContext = new();

            return await ApplicationUserDAL.IsAccessTokenValid(_dbContext, accessToken);
        }

        private string CreateAccessToken()
        {
            return $"{ Guid.NewGuid().ToString().Replace("-", "")}{ Guid.NewGuid().ToString().Replace("-", "")}{ Guid.NewGuid().ToString().Replace("-", "")}{ Guid.NewGuid().ToString().Replace("-", "")}{ Guid.NewGuid().ToString().Replace("-", "")}{ Guid.NewGuid().ToString().Replace("-", "")}";
        }

        private AuthenticateResp FillAuthenticateResp(AccessToken accessToken)
        {
            return new AuthenticateResp()
            {
                AccessToken = accessToken.AccessTokenValue,
                ExpiryDate = accessToken.ExpiryDate
            };
        }

        private SignInResp FillSignInResp(ApplicationUser applicationUser)
        {
            return new SignInResp()
            {
                Username = applicationUser.Username,
                ApplicationUserType = applicationUser.ApplicationUserType
            };
        }
    }
}
