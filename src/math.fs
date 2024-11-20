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

\ *** Array operations *** /
: PUT ( n addr idx# -- ) CELLS + ! ;
: GET ( addr idx# -- n ) CELLS + @ ;

\ *** Vector operations *** /
: v+ ( addr1 addr2 addr3 N -- addr3) { ADDR1 ADDR2 ADDR3 N }
    ADDR3 N 1 - CELLS ALLOT
    N 0 DO ADDR1 i GET ADDR2 i GET + ADDR3 i PUT LOOP ;
: v. ( addr1 addr2 addr3 N -- n ) { ADDR3 N }
    ADDR3 N v+ DROP 0 N 0 DO ADDR3 i GET + LOOP ;

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
