using System.Collections.Generic;

namespace Tests
{
    public class Example
    {
        private void NameMethod()
        {
            new User(new Purse()).Buy(new List<string>());
        }

        class User
        {
            private readonly IPay _purse;

            public User(IPay purse)
            {
                _purse = purse;
            }

            public void Buy(List<string> list)
            {
                // todo
            }

        }

        class Purse : IPay
        {
            public int Money { get; private set; }
        }

        interface IPay
        {
            int Money { get; }
        }

    }
}
