using System.Threading.Tasks;
namespace BloodBank.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string phoneNumber, string message);
    }
}
