\ Mathematics related functions
INCLUDE utils.fs

\ *** Indicies *** /
: ** ( n -- n ) DUP * ;
: ^ ( n1 n2 -- n ) OVER 2ROT 1 DO DUP ROT * SWAP LOOP DROP ;

\ *** Sumative and Cumulative *** /
: SUM ( n1 n2 ... nN N -- n ) 1 DO + LOOP ;
: PROD ( n1 n2 ... nN N -- n ) 1 DO * LOOP ;
: BINOM ( n -- n ) 1 1 2ROT DO i 1 + + LOOP ;
: FACT ( n -- n ) 1 1 2ROT DO i 1 + * LOOP ;

\ *** Statistics *** /
: MEAN ( n1 n2 ... nN N -- n ) DUP >R SUM R> / ;

\ *** Sequences and Series *** /
: FIB ( n -- n )
    DUP
    2 < IF DUP
    ELSE 0 1 ROT 1 DO
            DUP ROT +
        LOOP
    THEN SWAP DROP ;

\ *** Vector operations *** /
 : VADD ( n1 ... nN n1 ... nN N -- n1 .. nN N )
    DUP 0 DO 
        DUP 2* PICK SWAP DUP 1 + PICK ROT + SWAP
    LOOP
    \ Removes the input vectors from the stack
    \ DO keeps the limit and index on the return stack
    \ So we need to take those off before we can 
    DUP 0 DO SWAP R> R> ROT >R >R >R LOOP
    DUP 2* 0 DO SWAP DROP LOOP
    DUP 0 DO R> R> R> 2ROT >R >R SWAP LOOP ;
: v+ ( n1 ... nN n1 ... nN N -- n1 .. nN ) VADD DROP ;
: v. ( n1 ... nN n1 .. nN N -- n ) VADD SUM ;

\ *** Constants ***
\ I define non-integer constants as numerator/ /denominator
\ this might help concatenation of multiple constants
\ it's also helpful to have a word for the constant multiplied by something
355 CONSTANT PI/
113 CONSTANT /PI
: PI ( n -- n ) PI/ /PI */ ;

28667 CONSTANT e/
10546 CONSTANT /e
: e ( n -- n ) e/ /e */ ;

19601 CONSTANT sqrt2/
13860 CONSTANT /sqrt2
: sqrt2 ( n -- n ) sqrt2/ /sqrt2  */ ;

18817 CONSTANT sqrt3/
10864 CONSTANT /sqrt3
: sqrt3 ( n -- n ) sqrt3/ /sqrt3 */ ;

2040 CONSTANT log2/
11103 CONSTANT /log2
: log2 ( n -- n ) log2/ /log2 */ ;

485 CONSTANT ln2/
11464 CONSTANT /ln2
: ln2 ( n -- n ) ln2/ /ln2 */ ;
