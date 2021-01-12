using System.Threading.Tasks;

namespace ScrumRetroApp.API.Services
{
	public interface ITFSService
	{
		Task<string> GetProfilePicture(string strMail);
	}
}