\ Utility words and constants

\ General
: 2ROT ( a b c -- c a b) ROT ROT ;
: CLEAR ( n1 ... nN -- ) DEPTH 0 DO DROP LOOP ;
: VIEW-STACK ( -- )
    DEPTH 0 = IF
        CR ." Stack empty."
    ELSE
        DEPTH 0 DO
            CR i . ." : " i PICK .
        LOOP
    THEN ;

\ Indicies
: ** ( n -- n*n ) DUP * ;
: ^ ( n1 n2 -- n1^n2 ) OVER 2ROT 1 DO DUP ROT * SWAP LOOP DROP ;

\ Sumative and Cumulative
: SUM ( n1 n2 ... nN N -- n1+n2+...nN) 1 DO + LOOP ;
: PROD ( n1 n2 ... nN N -- n1*n2*...nN) 1 DO * LOOP ;
: BINOM ( n -- BINOM_COEF n ) 1 1 2ROT DO i 1 + + LOOP ;
: ! ( n -- n! ) 1 1 2ROT DO i 1 + * LOOP ;

\ Sequences and Series
: FIB ( n1 -- n2 )
    DUP
    2 < IF
    ELSE 0 1 ROT 1 DO
            DUP ROT +
        LOOP
    THEN ;

\ Vector operations
: VADD-NODROP ( n1 ... nN n1 ... nN N -- n1 .. nN N )
    DUP 0 DO 
        DUP 2 * PICK SWAP DUP 1 + PICK ROT + SWAP
    LOOP ;
: VADD ( n1 ... nN n1 ... nN N -- n1 .. nN ) VADD DROP ;
: DOT ( n1 ... nN n1 .. nN N -- n ) VADD-NODROP SUM ;

\ Constants
355e 113e f/ fCONSTANT Pi
271801e 99990e f/ fCONSTANT E
