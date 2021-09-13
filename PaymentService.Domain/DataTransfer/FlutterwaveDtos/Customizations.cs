namespace PaymentService.Domain.DataTransfer.FlutterwaveDtos
{
    public class Customizations
    {
        public Customizations()
        {
            Title = "Donation";
            LogoUrl = "https://res.cloudinary.com/cloud9ne/image/upload/v1631533757/Screenshot_188_s6nyrl.png";
            Description = "Help us help others";
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public string LogoUrl { get; set; }
    }
}