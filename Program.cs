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

            //Get and Validate user input
            try
            {
                //Cast input string as int
                num = int.Parse(Console.ReadLine());

                // Call CheckBalance method on the ep object
                // It will invoke the ep.evt delegate if the balance exceeds 250
                ep.CheckBalance(num);
            }
            catch (FormatException e) 
            {
                //Handle throwned format exception(common exception)
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                //Handle any other form of exception that is thrown (uncommon exceptions)
                Console.WriteLine("{0} Exception caught.", e);
            }
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
