using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Day9 {
  class Program {
    private static string inputTest = @"104,1125899906842624,99";
    private static string input = @"1102,34463338,34463338,63,1007,63,34463338,63,1005,63,53,1102,1,3,1000,109,988,209,12,9,1000,209,6,209,3,203,0,1008,1000,1,63,1005,63,65,1008,1000,2,63,1005,63,904,1008,1000,0,63,1005,63,58,4,25,104,0,99,4,0,104,0,99,4,17,104,0,99,0,0,1102,1,1,1021,1101,0,21,1009,1101,0,28,1005,1102,1,27,1015,1102,39,1,1016,1102,1,30,1003,1102,25,1,1007,1102,195,1,1028,1101,0,29,1010,1102,26,1,1004,1102,1,555,1024,1102,32,1,1014,1101,0,23,1019,1102,1,31,1008,1101,652,0,1023,1102,20,1,1000,1101,0,821,1026,1102,814,1,1027,1102,1,36,1017,1101,0,38,1006,1102,1,37,1011,1102,33,1,1001,1102,35,1,1013,1102,190,1,1029,1102,1,22,1018,1101,0,0,1020,1102,1,34,1012,1102,24,1,1002,1101,0,655,1022,1102,1,546,1025,109,37,2106,0,-9,4,187,1106,0,199,1001,64,1,64,1002,64,2,64,109,-32,1202,1,1,63,1008,63,38,63,1005,63,225,4,205,1001,64,1,64,1106,0,225,1002,64,2,64,109,6,1206,10,241,1001,64,1,64,1106,0,243,4,231,1002,64,2,64,109,-12,1207,2,32,63,1005,63,259,1106,0,265,4,249,1001,64,1,64,1002,64,2,64,109,2,2101,0,0,63,1008,63,33,63,1005,63,291,4,271,1001,64,1,64,1106,0,291,1002,64,2,64,109,21,1205,-1,305,4,297,1106,0,309,1001,64,1,64,1002,64,2,64,109,-10,2108,29,-7,63,1005,63,329,1001,64,1,64,1106,0,331,4,315,1002,64,2,64,109,-15,2107,26,10,63,1005,63,347,1106,0,353,4,337,1001,64,1,64,1002,64,2,64,109,13,21107,40,41,2,1005,1012,375,4,359,1001,64,1,64,1106,0,375,1002,64,2,64,109,7,21107,41,40,-5,1005,1012,391,1105,1,397,4,381,1001,64,1,64,1002,64,2,64,109,-6,21102,42,1,2,1008,1013,40,63,1005,63,421,1001,64,1,64,1105,1,423,4,403,1002,64,2,64,109,-10,2107,23,1,63,1005,63,441,4,429,1105,1,445,1001,64,1,64,1002,64,2,64,109,3,1201,5,0,63,1008,63,21,63,1005,63,467,4,451,1106,0,471,1001,64,1,64,1002,64,2,64,109,18,21108,43,43,-5,1005,1017,489,4,477,1105,1,493,1001,64,1,64,1002,64,2,64,109,-29,1207,7,21,63,1005,63,511,4,499,1106,0,515,1001,64,1,64,1002,64,2,64,109,23,21108,44,46,-6,1005,1010,531,1106,0,537,4,521,1001,64,1,64,1002,64,2,64,109,11,2105,1,-3,4,543,1001,64,1,64,1106,0,555,1002,64,2,64,109,-3,1205,-4,571,1001,64,1,64,1105,1,573,4,561,1002,64,2,64,109,-7,2108,21,-8,63,1005,63,595,4,579,1001,64,1,64,1105,1,595,1002,64,2,64,109,-1,1208,-8,28,63,1005,63,615,1001,64,1,64,1106,0,617,4,601,1002,64,2,64,109,-12,1202,4,1,63,1008,63,29,63,1005,63,641,1001,64,1,64,1106,0,643,4,623,1002,64,2,64,109,18,2105,1,1,1105,1,661,4,649,1001,64,1,64,1002,64,2,64,109,-6,2102,1,-8,63,1008,63,31,63,1005,63,687,4,667,1001,64,1,64,1106,0,687,1002,64,2,64,109,-7,21102,45,1,6,1008,1015,45,63,1005,63,709,4,693,1106,0,713,1001,64,1,64,1002,64,2,64,109,-6,2101,0,0,63,1008,63,31,63,1005,63,737,1001,64,1,64,1105,1,739,4,719,1002,64,2,64,109,7,1208,-8,24,63,1005,63,761,4,745,1001,64,1,64,1105,1,761,1002,64,2,64,109,-12,2102,1,10,63,1008,63,32,63,1005,63,781,1106,0,787,4,767,1001,64,1,64,1002,64,2,64,109,16,1206,6,801,4,793,1106,0,805,1001,64,1,64,1002,64,2,64,109,14,2106,0,-1,1001,64,1,64,1106,0,823,4,811,1002,64,2,64,109,-18,1201,-7,0,63,1008,63,27,63,1005,63,847,1001,64,1,64,1105,1,849,4,829,1002,64,2,64,109,-8,21101,46,0,10,1008,1012,46,63,1005,63,875,4,855,1001,64,1,64,1106,0,875,1002,64,2,64,109,13,21101,47,0,-3,1008,1012,44,63,1005,63,899,1001,64,1,64,1105,1,901,4,881,4,64,99,21101,27,0,1,21102,1,915,0,1105,1,922,21201,1,11564,1,204,1,99,109,3,1207,-2,3,63,1005,63,964,21201,-2,-1,1,21101,942,0,0,1105,1,922,22101,0,1,-1,21201,-2,-3,1,21101,0,957,0,1106,0,922,22201,1,-1,-2,1105,1,968,21202,-2,1,-2,109,-3,2105,1,0";
    static void Main( string[] args ) {
      Console.WriteLine( "Hello World!" );
      BigInteger[] program = input.Split( new[] { "," }, StringSplitOptions.RemoveEmptyEntries )
        .Select( BigInteger.Parse ).ToArray();

      var computer = new IntCodeComputer( program );

      if ( !computer.Execute( 2 ) ) {
        throw new InvalidOperationException();
      }

      Console.WriteLine( string.Join( ",", computer.outputs ) );

    }

    static void Main1( string[] args ) {
      Console.WriteLine( "Hello World!" );
      BigInteger[] program = input.Split( new[] { "," }, StringSplitOptions.RemoveEmptyEntries )
        .Select( BigInteger.Parse ).ToArray();

      var computer = new IntCodeComputer( program );

      if ( !computer.Execute( 1 ) ) {
        throw new InvalidOperationException();
      }

      Console.WriteLine( string.Join( ",", computer.outputs ) );

    }

     public struct OpCode {
      public int Code;
      public int OriginalCode;

      private List<PositionMode> parameterMode;

      public OpCode( int opCode ) {
        OriginalCode = opCode;
        parameterMode = new List<PositionMode>();
        Code = opCode % 100;
        opCode = opCode / 100;
        do {
          int mode = opCode % 10;

          parameterMode.Add( (PositionMode) mode  );
          opCode /= 10;
        } while ( opCode != 0 );
      }

      public PositionMode GetMode( int paramNumber ) {
        if ( paramNumber >= parameterMode.Count() ) {
          return PositionMode.Absolute;
        }
        return parameterMode[paramNumber];
      }
    }

     public enum PositionMode {
       Absolute = 0,
       None = 1,
       Relative = 2,

     }

    public class IntCodeComputer {
      private BigInteger[] program;
      private BigInteger[] ints;
      private Queue<BigInteger> inputs = new Queue<BigInteger>();
      public Queue<BigInteger> outputs = new Queue<BigInteger>();
      private int pointer = 0;
      public BigInteger RelativeBase = 0;

      public IntCodeComputer( BigInteger[] program ) {
        this.program = program;
        ints = new BigInteger[program.Length * 8];
        Array.Copy( program, ints, program.Length );
      }

      bool HasValue() {
        return pointer < ints.Length;
      }

      BigInteger GetValue( BigInteger val, PositionMode byValue = PositionMode.Absolute ) {
        switch ( byValue ) {
          case PositionMode.None:
            return val;
          case PositionMode.Absolute:
            return ints[(int) val];
          case PositionMode.Relative:
            return ints[(int) (val + RelativeBase)];
        }
        return byValue == PositionMode.None? val : ints[(int) val];
      }

      BigInteger NextValue( ) {
        return ints[pointer++];
      }

      void WriteValue( BigInteger val, BigInteger position, PositionMode byValue = PositionMode.Absolute ) {
        switch ( byValue ) {
          case PositionMode.None:
           throw new ArgumentException();
          case PositionMode.Absolute:
            ints[(int) position] = val;
            return;
          case PositionMode.Relative:
            ints[(int) (position + RelativeBase)] = val;
            return;
        }
      }


      /// <summary>
      /// true if terminated
      /// </summary>
      public bool Execute( params int[] input ) {
        foreach ( var i in input ) {
          inputs.Enqueue( i );
        }
        bool exit = false;
        while ( !exit && HasValue() ) {
          OpCode opCode = new OpCode((int) ints[pointer] );
          switch ( opCode.Code ) {
            case 1:
              Add(opCode);
              break;
            case 2:
              Multiply(opCode);
              break;
            case 3:
              if ( !Input( opCode ) ) {
                return false;
              };
              break;
            case 4:
              Ouput( opCode );
              break;
            case 5:
              JumpIfTrue( opCode );
              break;
            case 6:
              JumpIfFalse( opCode );
              break;
            case 7:
              LessThan( opCode );
              break;
            case 8:
              AreEqual( opCode );
              break;
            case 9:
              AdjustBase( opCode );
              break;
            case 99:
              exit = true;
              break;
            default:
              throw new InvalidOperationException();
          }
        }

        return true;

      }



      void Multiply( OpCode op) {
        pointer++;

        BigInteger first = NextValue();
        BigInteger second = NextValue();
        BigInteger third = NextValue();
        Console.Write( string.Join( ",", new []{op.OriginalCode,first,second,third} ) + " : ");

        BigInteger leftVal = GetValue( first, op.GetMode( 0 ) );
        BigInteger rightVal = GetValue( second, op.GetMode( 1 ) );
        var outVal = leftVal * rightVal;
        WriteValue( outVal, third, op.GetMode( 2 ));
        Console.WriteLine( $"{leftVal} * {rightVal} = {outVal} to {third}");
      }
      void Add( OpCode op ) {
        pointer++;
        BigInteger first = NextValue();
        BigInteger leftVal = GetValue( first, op.GetMode( 0 ) );
        BigInteger second = NextValue();
        BigInteger rightVal = GetValue( second, op.GetMode( 1 ) );
        var third = NextValue( );

        Console.Write( string.Join( ",", new []{op.OriginalCode,first,second,third} )+ " : ");

        var outVal = leftVal + rightVal;
        WriteValue( outVal,third,op.GetMode( 2 ));
        Console.WriteLine( $"{leftVal} + {rightVal} = {outVal} to {third}");
      }

      void JumpIfTrue( OpCode op ) {
        pointer++;

        BigInteger first = NextValue();
        BigInteger val =  GetValue( first, op.GetMode( 0 ) );
        var second = NextValue();
        Console.Write( string.Join( ",", new []{op.OriginalCode,first,second} )+ " : ");
        if ( val != BigInteger.Zero ) {
          pointer =  (int) GetValue( second, op.GetMode( 1 ) );
        }
        Console.WriteLine( $"{val} is true, jump to {pointer}");
      }
      void JumpIfFalse( OpCode op ) {
        pointer++;

        var first = NextValue();
        BigInteger val =  GetValue( first, op.GetMode( 0 ) );
        var second = NextValue();
        Console.Write( string.Join( ",", new []{op.OriginalCode,first,second} )+ " : ");
        if ( val == BigInteger.Zero ) {
          pointer =  (int) GetValue( second, op.GetMode( 1 ) );
        }
        Console.WriteLine( $"{val} is false, jump to {pointer}");
      }

      void LessThan( OpCode op ) {
        pointer++;

        var first = NextValue();
        BigInteger leftVal = GetValue( first, op.GetMode( 0 ) );
        var second = NextValue();
        BigInteger rightVal = GetValue( second, op.GetMode( 1 ) );
        var third = NextValue( );

        Console.Write( string.Join( ",", new []{op.OriginalCode,first,second,third} )+ " : ");

        WriteValue( leftVal < rightVal ? 1 : 0,third,op.GetMode( 2));

        Console.WriteLine( $"Compare less than {leftVal} to {rightVal} to {third}");
      }
      void AreEqual( OpCode op ) {
        pointer++;

        var first = NextValue();
        BigInteger leftVal = GetValue( first, op.GetMode( 0 ) );
        var second = NextValue();
        BigInteger rightVal = GetValue( second, op.GetMode( 1 ) );
        var third = NextValue( );
        Console.Write( string.Join( ",", new []{op.OriginalCode,first,second,third} )+ " : ");

        WriteValue( leftVal == rightVal ? 1 : 0,third,op.GetMode( 2));

        Console.WriteLine( $"Compare {leftVal} to {rightVal} to {third}");
      }

      private void AdjustBase( OpCode opCode ) {
        pointer++;
        var val = NextValue();
        RelativeBase += GetValue( val, opCode.GetMode( 0 ) );
      }

      bool Input( OpCode op ) {
        Console.Write( string.Join( ",", new []{op.OriginalCode,ints[pointer+1]} )+ " : ");

        if ( inputs.Count == 0 ) {
          Console.WriteLine( $"Wait for read input");

          return false;
        }
        var input = inputs.Dequeue();

        pointer++;

        var position = NextValue( );
        WriteValue( input, position, op.GetMode( 0 ));
        Console.WriteLine( $"Read input {input} and store in {position}");
        return true;
      }

      void Ouput( OpCode op ) {
        pointer++;

        var first = NextValue( );
        var value = GetValue( first, op.GetMode( 0 ) );

        Console.Write( string.Join( ",", new []{op.OriginalCode,first} )+ " : ");

        Console.WriteLine( $"Output {value}.");
        outputs.Enqueue( value );
      }
    }

  }
}