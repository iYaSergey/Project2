using Business_Layer;
using Data_Layer;

namespace Service_Layer
{
    public class Service : IService
    {
        public Service()
        {

        }

        static readonly IBusiness business = new Business();
    }
}
