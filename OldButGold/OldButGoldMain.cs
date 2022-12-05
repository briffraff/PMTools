using Global;
using OldButGold.services;

namespace OldButGold
{
    public class OldButGoldMain
    {
        OutputOldButGold _output = new OutputOldButGold();

        public void Power(Constants _gc)
        {
            _output.welcomeMessage();
        }
    }
}
