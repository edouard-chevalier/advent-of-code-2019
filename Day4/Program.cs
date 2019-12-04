using System;
using System.Collections.Generic;
using System.Linq;

namespace Day4 {
  class Program {
    static void Main( string[] args ) {
      Console.WriteLine( "Hello World!" );
      Console.WriteLine( " 111122:" + MeetCriteria2(  111122) );
      int count = 0;
      for ( int i = 273025; i <= 767253; i++ ) {
        if ( MeetCriteria2( i ) ) {
          count++;
        }
      }
      Console.WriteLine( count );

    }


    static bool MeetCriteria( int val ) {

      int[] digits = new int[6];

      for ( int i = 0; i < 6; i++ ) {
        digits[5 - i] = val % 10;
        val = val / 10;
      }
      int currentDigit = -1;
      bool isDoubled = false;
      foreach ( var digit in digits ) {
        isDoubled = isDoubled || ( currentDigit == digit );
        if ( digit < currentDigit ) {
          return false;
        }

        currentDigit = digit;
      }
      return isDoubled;
    }
    static bool MeetCriteria2( int val ) {

          int[] digits = new int[6];

          for ( int i = 0; i < 6; i++ ) {
            digits[5 - i] = val % 10;
            val = val / 10;
          }
          int currentDigit = -1;
          var matchingSeq = new List<int>();

          int seqLength = 0;
          foreach ( var digit in digits ) {

            if ( digit < currentDigit ) {
              return false;
            }

            if ( currentDigit == digit ) {
              seqLength++;
            }
            else {
              if ( seqLength > 1 ) {
                matchingSeq.Add(  seqLength);
              }

              seqLength = 1;
            }

            currentDigit = digit;
          }
          if ( seqLength > 1 ) {
            matchingSeq.Add(  seqLength);
          }
          return matchingSeq.Where( c => c == 2 ).Any();
        }
  }
}