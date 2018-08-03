

using System.IO;

namespace ArduGround
{
   class ArduinoSerial:ConnetActivity {
        ArduinoSerial(Stream _Input,Stream _Output)
        {
            Input = _Input;
            Output = _Output;
        }
        Stream Input;
        Stream Output;
        static void main()
        {
            Listen();
        }

        void Listen()
        {
            //
        }
    }
}
