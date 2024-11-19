\ Mathematics related functions
INCLUDE utils.fs

\ *** Indicies *** /
: ** ( n -- n ) DUP * ;
: ^ ( n1 n2 -- n3 ) OVER 2ROT 1 DO DUP ROT * SWAP LOOP DROP ;

\ *** Sumative and Cumulative *** /
: SUM ( n1 n2 ... nN N -- n1+n2+...nN) 1 DO + LOOP ;
: PROD ( n1 n2 ... nN N -- n1*n2*...nN) 1 DO * LOOP ;
: BINOM ( n -- n ) 1 1 2ROT DO i 1 + + LOOP ;
: ! ( n -- n ) 1 1 2ROT DO i 1 + * LOOP ;

\ *** Statistics *** /
: MEAN ( n1 n2 ... nN N ... n ) DUP >R SUM R> / ;

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
        DUP 2 * PICK SWAP DUP 1 + PICK ROT + SWAP
    LOOP
    \ Removes the input vectors from the stack
    \ DO keeps the limit and index on the return stack
    \ So we need to take those off before we can 
    DUP 0 DO SWAP R> R> ROT >R >R >R LOOP
    DUP 2 * 0 DO SWAP DROP LOOP
    DUP 0 DO R> R> R> 2ROT >R >R SWAP LOOP ;
: v+ ( n1 ... nN n1 ... nN N -- n1 .. nN ) VADD DROP ;
: v. ( n1 ... nN n1 .. nN N -- n ) VADD SUM ;

\ *** Multiply by Constants *** /
: PI ( n -- n ) 355 113 */ ;
: sqrt2 ( n -- n ) 19601 13860 */ ;
: sqrt3 ( n -- n ) 18817 10864 */ ;
: e ( n -- n ) 28667 10546 */ ;
: log2 ( n -- n ) 2040 11103 */ ;
: ln2 ( n -- n ) 485 11464 */ ;
