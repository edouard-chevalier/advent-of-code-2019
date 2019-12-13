using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Day13 {

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

      private bool debug = false;

      public IntCodeComputer( string input ) : this( input.Split( new[] { "," }, StringSplitOptions.RemoveEmptyEntries )
        .Select( BigInteger.Parse ).ToArray()){
      }
      internal IntCodeComputer( BigInteger[] program ) {
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
        if (debug) Console.Write( string.Join( ",", new []{op.OriginalCode,first,second,third} ) + " : ");

        BigInteger leftVal = GetValue( first, op.GetMode( 0 ) );
        BigInteger rightVal = GetValue( second, op.GetMode( 1 ) );
        var outVal = leftVal * rightVal;
        WriteValue( outVal, third, op.GetMode( 2 ));
        if (debug) Console.WriteLine( $"{leftVal} * {rightVal} = {outVal} to {third}");
      }
      void Add( OpCode op ) {
        pointer++;
        BigInteger first = NextValue();
        BigInteger leftVal = GetValue( first, op.GetMode( 0 ) );
        BigInteger second = NextValue();
        BigInteger rightVal = GetValue( second, op.GetMode( 1 ) );
        var third = NextValue( );

        if (debug)Console.Write( string.Join( ",", new []{op.OriginalCode,first,second,third} )+ " : ");

        var outVal = leftVal + rightVal;
        WriteValue( outVal,third,op.GetMode( 2 ));
        if (debug) Console.WriteLine( $"{leftVal} + {rightVal} = {outVal} to {third}");
      }

      void JumpIfTrue( OpCode op ) {
        pointer++;

        BigInteger first = NextValue();
        BigInteger val =  GetValue( first, op.GetMode( 0 ) );
        var second = NextValue();
        if (debug) Console.Write( string.Join( ",", new []{op.OriginalCode,first,second} )+ " : ");
        if ( val != BigInteger.Zero ) {
          pointer =  (int) GetValue( second, op.GetMode( 1 ) );
        }
        if (debug) Console.WriteLine( $"{val} is true, jump to {pointer}");
      }
      void JumpIfFalse( OpCode op ) {
        pointer++;

        var first = NextValue();
        BigInteger val =  GetValue( first, op.GetMode( 0 ) );
        var second = NextValue();
        if (debug) Console.Write( string.Join( ",", new []{op.OriginalCode,first,second} )+ " : ");
        if ( val == BigInteger.Zero ) {
          pointer =  (int) GetValue( second, op.GetMode( 1 ) );
        }
        if (debug) Console.WriteLine( $"{val} is false, jump to {pointer}");
      }

      void LessThan( OpCode op ) {
        pointer++;

        var first = NextValue();
        BigInteger leftVal = GetValue( first, op.GetMode( 0 ) );
        var second = NextValue();
        BigInteger rightVal = GetValue( second, op.GetMode( 1 ) );
        var third = NextValue( );

        if (debug) Console.Write( string.Join( ",", new []{op.OriginalCode,first,second,third} )+ " : ");

        WriteValue( leftVal < rightVal ? 1 : 0,third,op.GetMode( 2));

        if (debug) Console.WriteLine( $"Compare less than {leftVal} to {rightVal} to {third}");
      }
      void AreEqual( OpCode op ) {
        pointer++;

        var first = NextValue();
        BigInteger leftVal = GetValue( first, op.GetMode( 0 ) );
        var second = NextValue();
        BigInteger rightVal = GetValue( second, op.GetMode( 1 ) );
        var third = NextValue( );
        if (debug)  Console.Write( string.Join( ",", new []{op.OriginalCode,first,second,third} )+ " : ");

        WriteValue( leftVal == rightVal ? 1 : 0,third,op.GetMode( 2));

        if (debug) Console.WriteLine( $"Compare {leftVal} to {rightVal} to {third}");
      }

      private void AdjustBase( OpCode opCode ) {
        pointer++;
        var val = NextValue();
        RelativeBase += GetValue( val, opCode.GetMode( 0 ) );
      }

      bool Input( OpCode op ) {
        if (debug) Console.Write( string.Join( ",", new []{op.OriginalCode,ints[pointer+1]} )+ " : ");

        if ( inputs.Count == 0 ) {
          /*if (debug)*/ Console.WriteLine( $"Wait for read input");

          return false;
        }
        var input = inputs.Dequeue();

        pointer++;

        var position = NextValue( );
        WriteValue( input, position, op.GetMode( 0 ));
        /*if (debug)*/Console.WriteLine( $"Read input {input} and store in {position}");
        return true;
      }

      void Ouput( OpCode op ) {
        pointer++;

        var first = NextValue( );
        var value = GetValue( first, op.GetMode( 0 ) );

        if (debug) Console.Write( string.Join( ",", new []{op.OriginalCode,first} )+ " : ");

        if (debug) Console.WriteLine( $"Output {value}.");
        outputs.Enqueue( value );
      }
    }
}