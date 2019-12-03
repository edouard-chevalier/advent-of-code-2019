using System;
using System.Linq;

namespace Day2bis {
  class Program {
    static string input = @"1,12,2,3,1,1,2,3,1,3,4,3,1,5,0,3,2,6,1,19,1,19,5,23,2,10,23,27,2,27,13,31,1,10,31,35,1,35,9,39,2,39,13,43,1,43,5,47,1,47,6,51,2,6,51,55,1,5,55,59,2,9,59,63,2,6,63,67,1,13,67,71,1,9,71,75,2,13,75,79,1,79,10,83,2,83,9,87,1,5,87,91,2,91,6,95,2,13,95,99,1,99,5,103,1,103,2,107,1,107,10,0,99,2,0,14,0";

    static void Main( string[] args ) {
      Console.WriteLine( "Hello World!" );
      int[] program = input.Split( new[] { "," }, StringSplitOptions.RemoveEmptyEntries )
        .Select( Int32.Parse ).ToArray();

      for ( int noun = 0; noun <= 99; noun++ ) {
        for ( int verb = 0; verb <= 99; verb++ ) {
          int res = executeProgram( program, noun, verb );
          if ( res == 19690720 ) {
            Console.Write( 100 * noun + verb );
          }
        }
      }
    }

    static int executeProgram( int[] program, int a, int b ) {
      int[] ints = new int[program.Length];
      Array.Copy( program, ints, program.Length );
      ints[1] = a;
      ints[2] = b;


      int pointer = 0;

      bool HasValue() {
        return pointer < ints.Length;
      }
      int NextValue() {
        return ints[pointer++];
      }


      while ( HasValue() ) {
        int opCode = NextValue();
        if ( opCode == 99 ) {
          break;
        }

        if ( opCode == 1 || opCode == 2 ) {
          int leftVal = ints[ NextValue() ];
          int rightVal = ints[ NextValue() ];
          int finalPos = NextValue();
          if ( opCode == 1 ) {
            ints[finalPos] = leftVal + rightVal;
          }
          else {
            ints[finalPos] = leftVal * rightVal;
          }
        }
        else {
          throw new InvalidOperationException();
        }
      }

      return ints[0];
    }

    static void First() {

     string inputTest = @"1,1,1,4,99,5,6,0,99";

      int[] ints = input.Split( new[] { "," }, StringSplitOptions.RemoveEmptyEntries )
        .Select( Int32.Parse ).ToArray();
/*
      int pointer = 0;

      bool HasValue() {
        return pointer < ints.Length;
      }
      int NextValue() {
        return ints[pointer++];
      }


      while ( HasValue() ) {
        int opCode = NextValue();
        if ( opCode == 99 ) {
          break;
        }

        if ( opCode == 1 || opCode == 2 ) {
          int leftVal = ints[ NextValue() ];
          int rightVal = ints[ NextValue() ];
          int finalPos = NextValue();
          if ( opCode == 1 ) {
            ints[finalPos] = leftVal + rightVal;
          }
          else {
            ints[finalPos] = leftVal * rightVal;
          }
        }
        else {
          throw new InvalidOperationException();
        }
      }

      Console.Write( string.Join( ',', ints ) );
      */
     Console.Write( executeProgram( ints, 12, 2 ) );

    }
  }
}