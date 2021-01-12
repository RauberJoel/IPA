using System.Threading.Tasks;

namespace ScrumRetroApp.Blazor.Services
{
	public interface ITFSService
	{
		Task<string> GetProfilePicture(string strMail);
	}
}