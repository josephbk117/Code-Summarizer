using System;
using System.IO;
using System.Drawing;
using System.Collections;

namespace CowBang
{
  class Hello : MonoBehaviour
  {
    //TODO find and repair

    bool k;
    public int h = 66;
    int c = 55;
    static int a = 26;
    int x = 35;
    const float zam = 6.5f;

    protected void testFunction1()
    {
      Console.WriteLine("Helllo");
      if(true)
        return;

    }
    //just a normal comm
    //TODO vales are stored correctly
    public int testFunction2()
    {
      Console.WriteLine("Add");
      return 4;
    }
    private float testFunction3(int b)
    {
      int innerVal = 4;
      int nextVal = 7;
      Console.WriteLine("Sub" + b);
      return 1;
    }
    int inbetween;
    Bitmap ShowImage(bool isSet, int yDist)
    {
      Console.WriteLine("Sub" + b);
      return new Bitmap(20,40);
    }
    bool Catch(int p)
    {
      return true;
    }

  }
}
