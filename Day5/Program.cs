using System;
using System.Collections.Generic;
using System.Linq;

namespace Day5 {
  class Program {
    private static string isEqualTo8 = @"3,3,1107,-1,8,3,4,3,99";
    private static string testJump = @"3,3,1105,-1,9,1101,0,0,12,4,12,99,1";
    private static string moreComplex = @"3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,
1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,
999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99";
    static string input = @"3,225,1,225,6,6,1100,1,238,225,104,0,1101,37,61,225,101,34,121,224,1001,224,-49,224,4,224,102,8,223,223,1001,224,6,224,1,224,223,223,1101,67,29,225,1,14,65,224,101,-124,224,224,4,224,1002,223,8,223,101,5,224,224,1,224,223,223,1102,63,20,225,1102,27,15,225,1102,18,79,224,101,-1422,224,224,4,224,102,8,223,223,1001,224,1,224,1,223,224,223,1102,20,44,225,1001,69,5,224,101,-32,224,224,4,224,1002,223,8,223,101,1,224,224,1,223,224,223,1102,15,10,225,1101,6,70,225,102,86,40,224,101,-2494,224,224,4,224,1002,223,8,223,101,6,224,224,1,223,224,223,1102,25,15,225,1101,40,67,224,1001,224,-107,224,4,224,102,8,223,223,101,1,224,224,1,223,224,223,2,126,95,224,101,-1400,224,224,4,224,1002,223,8,223,1001,224,3,224,1,223,224,223,1002,151,84,224,101,-2100,224,224,4,224,102,8,223,223,101,6,224,224,1,224,223,223,4,223,99,0,0,0,677,0,0,0,0,0,0,0,0,0,0,0,1105,0,99999,1105,227,247,1105,1,99999,1005,227,99999,1005,0,256,1105,1,99999,1106,227,99999,1106,0,265,1105,1,99999,1006,0,99999,1006,227,274,1105,1,99999,1105,1,280,1105,1,99999,1,225,225,225,1101,294,0,0,105,1,0,1105,1,99999,1106,0,300,1105,1,99999,1,225,225,225,1101,314,0,0,106,0,0,1105,1,99999,108,677,677,224,1002,223,2,223,1006,224,329,101,1,223,223,1107,677,226,224,102,2,223,223,1006,224,344,101,1,223,223,8,677,677,224,1002,223,2,223,1006,224,359,101,1,223,223,1008,677,677,224,1002,223,2,223,1006,224,374,101,1,223,223,7,226,677,224,1002,223,2,223,1006,224,389,1001,223,1,223,1007,677,677,224,1002,223,2,223,1006,224,404,1001,223,1,223,7,677,677,224,1002,223,2,223,1006,224,419,1001,223,1,223,1008,677,226,224,1002,223,2,223,1005,224,434,1001,223,1,223,1107,226,677,224,102,2,223,223,1005,224,449,1001,223,1,223,1008,226,226,224,1002,223,2,223,1006,224,464,1001,223,1,223,1108,677,677,224,102,2,223,223,1006,224,479,101,1,223,223,1108,226,677,224,1002,223,2,223,1006,224,494,1001,223,1,223,107,226,226,224,1002,223,2,223,1006,224,509,1001,223,1,223,8,226,677,224,102,2,223,223,1006,224,524,1001,223,1,223,1007,226,226,224,1002,223,2,223,1006,224,539,1001,223,1,223,107,677,677,224,1002,223,2,223,1006,224,554,1001,223,1,223,1107,226,226,224,102,2,223,223,1005,224,569,101,1,223,223,1108,677,226,224,1002,223,2,223,1006,224,584,1001,223,1,223,1007,677,226,224,1002,223,2,223,1005,224,599,101,1,223,223,107,226,677,224,102,2,223,223,1005,224,614,1001,223,1,223,108,226,226,224,1002,223,2,223,1005,224,629,101,1,223,223,7,677,226,224,102,2,223,223,1005,224,644,101,1,223,223,8,677,226,224,102,2,223,223,1006,224,659,1001,223,1,223,108,677,226,224,102,2,223,223,1005,224,674,1001,223,1,223,4,223,99,226";

    static void Main( string[] args ) {
      Console.WriteLine( "Hello World!" );
      int[] program = input.Split( new[] { "," }, StringSplitOptions.RemoveEmptyEntries )
        .Select( Int32.Parse ).ToArray();
      var state = new State( program );
      state.Execute( 5 );
      Console.WriteLine( string.Join( ',',state.outputs) );
    }

    public struct OpCode {
      public int Code;
      public int OriginalCode;

      private List<bool> parameterMode;

      public OpCode( int opCode ) {
        OriginalCode = opCode;
        parameterMode = new List<bool>();
        Code = opCode % 100;
        opCode = opCode / 100;
        do {
          int mode = opCode % 10;
          parameterMode.Add( mode > 0  );
          opCode /= 10;
        } while ( opCode != 0 );
      }

      public bool GetMode( int paramNumber ) {
        if ( paramNumber >= parameterMode.Count() ) {
          return false;
        }

        return parameterMode[paramNumber];
      }
    }

    public class State {
      private int[] program;
      private int[] ints;
      private Stack<int> inputs = new Stack<int>();
      public List<int> outputs = new List<int>();
      private int pointer = 0;

      public State( int[] program ) {
        this.program = program;
      }

      bool HasValue() {
        return pointer < ints.Length;
      }

      int GetValue( int val, bool byValue = false ) {
        return byValue ? val : ints[val];
      }

      int NextValue( ) {
        return ints[pointer++];
      }
      void WriteValue( int val, int position ) {
        ints[position] = val;
      }

      public void Execute( params int[] input ) {
        ints = new int[program.Length];
        Array.Copy( program, ints, program.Length );
        foreach ( var i in input ) {
          inputs.Push( i );
        }
        pointer = 0;
        bool exit = false;
        while ( !exit && HasValue() ) {
          OpCode opCode = new OpCode(ints[pointer++] );
          switch ( opCode.Code ) {
            case 1:
              Add(opCode);
              break;
            case 2:
              Multiply(opCode);
              break;
            case 3:
              Input( opCode );
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
            case 99:
              exit = true;
              break;
            default:
              throw new InvalidOperationException();
          }
        }

      }

      void Multiply( OpCode op) {
        int first = NextValue();
        int second = NextValue();
        int third = NextValue();
        Console.Write( string.Join( ",", new []{op.OriginalCode,first,second,third} ) + " : ");

        int leftVal = GetValue( first, op.GetMode( 0 ) );
        int rightVal = GetValue( second, op.GetMode( 1 ) );
        var outVal = leftVal * rightVal;
        WriteValue( outVal, third);
        Console.WriteLine( $"{leftVal} * {rightVal} = {outVal} to {third}");
      }
      void Add( OpCode op ) {
        int first = NextValue();
        int leftVal = GetValue( first, op.GetMode( 0 ) );
        var second = NextValue();
        int rightVal = GetValue( second, op.GetMode( 1 ) );
        var third = NextValue( );

        Console.Write( string.Join( ",", new []{op.OriginalCode,first,second,third} )+ " : ");

        var outVal = leftVal + rightVal;
        WriteValue( outVal,third);
        Console.WriteLine( $"{leftVal} + {rightVal} = {outVal} to {third}");
      }

      void JumpIfTrue( OpCode op ) {
        var first = NextValue();
        int val =  GetValue( first, op.GetMode( 0 ) );
        var second = NextValue();
        Console.Write( string.Join( ",", new []{op.OriginalCode,first,second} )+ " : ");
        if ( val != 0 ) {
          pointer =  GetValue( second, op.GetMode( 1 ) );
        }
        Console.WriteLine( $"{val} is true, jump to {pointer}");
      }
      void JumpIfFalse( OpCode op ) {
        var first = NextValue();
        int val =  GetValue( first, op.GetMode( 0 ) );
        var second = NextValue();
        Console.Write( string.Join( ",", new []{op.OriginalCode,first,second} )+ " : ");
        if ( val == 0 ) {
          pointer =  GetValue( second, op.GetMode( 1 ) );
        }
        Console.WriteLine( $"{val} is false, jump to {pointer}");
      }

      void LessThan( OpCode op ) {
        var first = NextValue();
        int leftVal = GetValue( first, op.GetMode( 0 ) );
        var second = NextValue();
        int rightVal = GetValue( second, op.GetMode( 1 ) );
        var third = NextValue( );

        Console.Write( string.Join( ",", new []{op.OriginalCode,first,second,third} )+ " : ");

        WriteValue( leftVal < rightVal ? 1 : 0,third);

        Console.WriteLine( $"Compare less than {leftVal} to {rightVal} to {third}");
      }
      void AreEqual( OpCode op ) {
        var first = NextValue();
        int leftVal = GetValue( first, op.GetMode( 0 ) );
        var second = NextValue();
        int rightVal = GetValue( second, op.GetMode( 1 ) );
        var third = NextValue( );
        Console.Write( string.Join( ",", new []{op.OriginalCode,first,second,third} )+ " : ");

        WriteValue( leftVal == rightVal ? 1 : 0,third);

        Console.WriteLine( $"Compare {leftVal} to {rightVal} to {third}");
      }

      void Input( OpCode op ) {
        WriteValue( inputs.Pop(), NextValue( ));
      }

      void Ouput( OpCode op ) {
        var value = GetValue( NextValue( ), op.GetMode( 0 ) );
        if ( value != 0 ) {
          Console.WriteLine(pointer);
        }
        outputs.Add( value );
      }
    }
  }
}