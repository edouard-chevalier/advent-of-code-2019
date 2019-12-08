using System;
using System.Collections.Generic;
using System.Linq;

namespace Day7 {
  class Program {
    static void MainOld( string[] args ) {
      Console.WriteLine( "Hello World!" );
    }

    private static string inputTest = @"3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,
-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,
53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10";

    private static string input =
      @"3,8,1001,8,10,8,105,1,0,0,21,34,47,72,81,102,183,264,345,426,99999,3,9,102,5,9,9,1001,9,3,9,4,9,99,3,9,101,4,9,9,1002,9,3,9,4,9,99,3,9,102,3,9,9,101,2,9,9,102,5,9,9,1001,9,3,9,1002,9,4,9,4,9,99,3,9,101,5,9,9,4,9,99,3,9,101,3,9,9,1002,9,5,9,101,4,9,9,102,2,9,9,4,9,99,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,1,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,1,9,9,4,9,99,3,9,101,1,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,2,9,9,4,9,99,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,99,3,9,1001,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,99,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,1,9,4,9,99";

    static void Main( string[] args ) {
      Console.WriteLine( "Hello World!" );
      int[] program = input.Split( new[] { "," }, StringSplitOptions.RemoveEmptyEntries )
        .Select( Int32.Parse ).ToArray();

      int[] settings = new int[] { 5,6,7,8,9 };
      //int[] settings = new int[] { 9,8,7,6,5 };
      int maxOutPut = Int32.MinValue;
      int[] maxSettings = new int[5];
      do {
        State[] amplifiers = new State[5];
        for ( int i = 0; i < settings.Length; i++ ) {
          amplifiers[i] = new State( program );
          amplifiers[i].Execute( settings[i]);
        }


        int output = 0;
        int finalOutPut = 0;

        bool loop = true;
        int amplifierNo = 0;
        do {
          bool res = amplifiers[amplifierNo].Execute( output );
          output = amplifiers[amplifierNo].outputs.Dequeue();

          if ( res && amplifierNo == 4 ) {
              finalOutPut = output;
              loop = false;
          }

          amplifierNo++;
          amplifierNo %= 5;

        } while ( loop );

        if ( finalOutPut > maxOutPut ) {
          Array.Copy( settings, maxSettings, 5 );
          maxOutPut = output;
        }
      } while ( !NextPermutation( settings ) );

      Console.WriteLine( "" + maxOutPut + " with " + string.Join( ',',maxSettings) );
      /*int output2 = 0;
      for ( int i = 0; i < settings.Length; i++ ) {
        var state = new State( program );
        state.Execute( maxSettings[i], output2 );
        output2 = state.outputs.Last();
      }*/
    }
    /*static void Main1( string[] args ) {
      Console.WriteLine( "Hello World!" );
      int[] program = input.Split( new[] { "," }, StringSplitOptions.RemoveEmptyEntries )
        .Select( Int32.Parse ).ToArray();

      int[] settings = new int[] { 0, 1, 2, 3, 4 };
      int maxOutPut = Int32.MinValue;
      int[] maxSettings = new int[5];
      do {
        int output = 0;
        for ( int i = 0; i < settings.Length; i++ ) {
          var state = new State( program );
          state.Execute( settings[i], output );
          output = state.outputs.Dequeue();
        }

        if ( output > maxOutPut ) {
          Array.Copy( settings, maxSettings, 5 );
          maxOutPut = output;
        }
      } while ( !NextPermutation( settings ) );

      Console.WriteLine( "" + maxOutPut + " with " + string.Join( ',',maxSettings) );
      /*int output2 = 0;
      for ( int i = 0; i < settings.Length; i++ ) {
        var state = new State( program );
        state.Execute( maxSettings[i], output2 );
        output2 = state.outputs.Last();
      }
    }*/

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
      private Queue<int> inputs = new Queue<int>();
      public Queue<int> outputs = new Queue<int>();
      private int pointer = 0;

      public State( int[] program ) {
        this.program = program;
        ints = new int[program.Length];
        Array.Copy( program, ints, program.Length );
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

      public void AddInput( int input ) {
        inputs.Enqueue( input );
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
          OpCode opCode = new OpCode(ints[pointer] );
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
        pointer++;
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
        pointer++;

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
        pointer++;

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
        pointer++;

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
        pointer++;

        var first = NextValue();
        int leftVal = GetValue( first, op.GetMode( 0 ) );
        var second = NextValue();
        int rightVal = GetValue( second, op.GetMode( 1 ) );
        var third = NextValue( );
        Console.Write( string.Join( ",", new []{op.OriginalCode,first,second,third} )+ " : ");

        WriteValue( leftVal == rightVal ? 1 : 0,third);

        Console.WriteLine( $"Compare {leftVal} to {rightVal} to {third}");
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
        WriteValue( input, position);
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

    public static bool NextPermutation<T>( T[] elements ) where T : IComparable<T> {
      // More efficient to have a variable instead of accessing a property
      var count = elements.Length;

      // Indicates whether this is the last lexicographic permutation
      var done = true;

      // Go through the array from last to first
      for ( var i = count - 1; i > 0; i-- ) {
        var curr = elements[i];

        // Check if the current element is less than the one before it
        if ( curr.CompareTo( elements[i - 1] ) < 0 ) {
          continue;
        }

        // An element bigger than the one before it has been found,
        // so this isn't the last lexicographic permutation.
        done = false;

        // Save the previous (bigger) element in a variable for more efficiency.
        var prev = elements[i - 1];

        // Have a variable to hold the index of the element to swap
        // with the previous element (the to-swap element would be
        // the smallest element that comes after the previous element
        // and is bigger than the previous element), initializing it
        // as the current index of the current item (curr).
        var currIndex = i;

        // Go through the array from the element after the current one to last
        for ( var j = i + 1; j < count; j++ ) {
          // Save into variable for more efficiency
          var tmp = elements[j];

          // Check if tmp suits the "next swap" conditions:
          // Smallest, but bigger than the "prev" element
          if ( tmp.CompareTo( curr ) < 0 && tmp.CompareTo( prev ) > 0 ) {
            curr = tmp;
            currIndex = j;
          }
        }

        // Swap the "prev" with the new "curr" (the swap-with element)
        elements[currIndex] = prev;
        elements[i - 1] = curr;

        // Reverse the order of the tail, in order to reset it's lexicographic order
        for ( var j = count - 1; j > i; j--, i++ ) {
          var tmp = elements[j];
          elements[j] = elements[i];
          elements[i] = tmp;
        }

        // Break since we have got the next permutation
        // The reason to have all the logic inside the loop is
        // to prevent the need of an extra variable indicating "i" when
        // the next needed swap is found (moving "i" outside the loop is a
        // bad practice, and isn't very readable, so I preferred not doing
        // that as well).
        break;
      }

      // Return whether this has been the last lexicographic permutation.
      return done;
    }
  }
}