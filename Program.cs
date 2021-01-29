using System;

namespace BasicEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            //Local variables
            int num = 0;

            //Instantiate an event publisher object

            EvtPublisher ep = new EvtPublisher();

            // instantiate an event subscriber object
            evtSubscriber es = new evtSubscriber();

            ep.evt += es.HandleTheEvent;

            Console.WriteLine("Please enter a number");

            //Get user input
            try
            {
                num = int.Parse(Console.ReadLine());
            }
            catch (FormatException e) 
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }

            // Call CheckBalance method on the ep object
            // It will invoke the ep.evt delegate if the balance exceeds 250
            ep.CheckBalance(num);
        }
    }

    public class EvtPublisher
    {
        //custom event handler:
        public EventHandler<EvtArgsClass> evt;
        public void CheckBalance(int x)
        {
            if (x > 250)
            {
                EvtArgsClass eac = new EvtArgsClass("Balance exceeds over 250...");
                evt(this, eac);
            }
            else 
            {
                EvtArgsClass eac = new EvtArgsClass("Balance does not exceed over 250...");
                evt(this, eac);
            }
        }
    }

    //Subscriber
    public class evtSubscriber
    {
        public void HandleTheEvent(object sender, EvtArgsClass e)
        {
            Console.WriteLine("Attention! " + sender + ": " + e.Message);
            Console.ReadLine();
        }
    }

    public class EvtArgsClass : EventArgs
    {
        public EvtArgsClass(string str)
        {
            msg = str;
        }

        private string msg;
        public string Message
        {
            get { return msg; }
        }
    }
}
