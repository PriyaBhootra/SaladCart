namespace SaladCart.Models.DTOs
{

    public interface Ipayment
    {
        void Pay(Double amount);
    }

    public class CreditCardPayment : Ipayment
    {
        public void Pay(Double amount)
        {
            //Implentation
        }
    }

    public class UPIPayment : Ipayment
    {
        public void Pay(Double amount)
        {
            //Implentation
        }
    }

    public class NetBankingPayment : Ipayment
    {
        public void Pay(Double amount)
        {
            //Implentation
        }
    }
    public class CODPayment : Ipayment
    {
        public void Pay(Double amount)
        {
            //Implentation
        }
    }

    public class PaymentFactory 
    {

        public static Ipayment getPaymentMethod(string method)
        {
            switch (method)
            {
                case "CreditCard":
                    return new CreditCardPayment();

                case "NetBanking":
                    return new NetBankingPayment();

                case "UPI":
                    return new UPIPayment();

                case "COD":
                    return new CODPayment();

                default:
                    throw new ArgumentException("InvalidPaymentMethod");
            }
        }
    } 
}
