using Microsoft.EntityFrameworkCore;
using NetcareDoctorsAPI.Data.Entities;

namespace NetcareDoctorsAPI.Data.DALClasses
{
    public class ApplicationUserDAL
    {
        public async Task<bool> IsAccessTokenValid(NetcareDoctorsContext dbContext, string accessTokenValue)
        {
            return await (from accessToken in dbContext.AccessTokens
                          where accessToken.AccessTokenValue == accessTokenValue &&
                                accessToken.ExpiryDate >= DateTime.Now.Date
                          select accessToken).
                          AnyAsync();
        }

        public async Task<bool> IsAdministratorUsernameExisting(NetcareDoctorsContext dbContext, string username)
        {
            return await (from applicationUser in dbContext.ApplicationUsers
                          where applicationUser.Username == username &&
                                applicationUser.ApplicationUserType == FirmamentUtilities.Utilities.GetEnumDescription(NetcareDoctorsAPIEnum.ApplicationUserType.Administrator)
                          select applicationUser).
                          AnyAsync();
        }

        public async Task<ApplicationUser?> GetApplicationUserByUsername(NetcareDoctorsContext dbContext, string username)
        {
            return await (from applicationUser in dbContext.ApplicationUsers
                          where applicationUser.Username == username
                          select applicationUser).
                          FirstOrDefaultAsync();
        }

        public async Task<ApplicationUser?> GetApplicationUserByUsernameAndUserPassword(NetcareDoctorsContext dbContext, string username, string userPassword)
        {
            return await (from applicationUser in dbContext.ApplicationUsers
                          where applicationUser.Username == username &&
                                applicationUser.UserPassword == userPassword
                          select applicationUser).
                          FirstOrDefaultAsync();
        }

        //If change this function also change GetApplicationUserByUsername function
        public async Task<bool> IsUsernameExisting(NetcareDoctorsContext dbContext, string username)
        {
            return await (from applicationUser in dbContext.ApplicationUsers
                          where applicationUser.Username == username
                          select applicationUser).
                          AnyAsync();
        }
    }
}
