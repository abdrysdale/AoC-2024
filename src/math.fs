\ mathematics related functions
include utils.fs

\ *** indicies *** /
: ** ( n -- n ) dup * ;
: ^ ( n1 n2 -- n ) over 2rot 1 do dup rot * swap loop drop ;

\ *** sumative and cumulative *** /
: sum ( n1 n2 ... nN N -- n ) 1 do + loop ;
: prod ( n1 n2 ... nN N -- n ) 1 do * loop ;
: binom ( n -- n ) 1 1 2rot do i 1 + + loop ;
: fact ( n -- n ) 1 1 2rot do i 1 + * loop ;

\ *** statistics *** /
: mean ( n1 n2 ... nN N -- n ) dup >r sum r> / ;

\ *** sequences and series *** /
: fib ( n -- n )
    dup
    2 < if dup
    else 0 1 rot 1 do
            dup rot +
        loop
    then swap drop ;

\ *** array operations *** /
: put ( n addr idx# -- ) cells + ! ;
: get ( addr idx# -- n ) cells + @ ;

\ *** constants ***
\ I define non-integer constants as numerator/ /denominator
\ this might help concatenation of multiple constants
\ it's also helpful to have a word for the constant multiplied by something
355 constant pi/
113 constant /pi
: pi ( n -- n ) pi/ /pi */ ;

28667 constant e/
10546 constant /e
: e ( n -- n ) e/ /e */ ;

19601 constant sqrt2/
13860 constant /sqrt2
: sqrt2 ( n -- n ) sqrt2/ /sqrt2  */ ;

18817 constant sqrt3/
10864 constant /sqrt3
: sqrt3 ( n -- n ) sqrt3/ /sqrt3 */ ;

2040 constant log2/
11103 constant /log2
: log2 ( n -- n ) log2/ /log2 */ ;

485 constant ln2/
11464 constant /ln2
: ln2 ( n -- n ) ln2/ /ln2 */ ;
