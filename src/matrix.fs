\ Matrix related functions
\ Matrices are stored in the following way:
\   [ M N row-stride col-stride pointer-to-data ]
\ and initialised matrices are assumed to be row contiguous.

include math.fs

\ Convience function for matrix properties
0 constant row-idx 1 constant col-idx
2 constant rs-idx 3 constant cs-idx
4 constant dat-idx
: rows ( addr -- n ) row-idx get ;
: cols ( addr -- n ) col-idx get ;
: rstride ( addr -- n ) rs-idx get ;
: cstride ( addr -- n ) cs-idx get ;
: mdata ( addr1 -- addr2 ) dat-idx get ;
: mdget ( addr idx -- n ) { addr idx } addr mdata idx get ;
: mdput ( val addr idx -- n ) { addr idx } addr mdata idx put ;
: m-idx ( addr i j -- idx ) { addr i j } addr rstride i * addr cstride j * + ;
: m-val ( addr i j -- val ) { addr i j } addr mdata addr i j m-idx get ;
: m-store ( n addr i j -- ) { addr i j } addr addr i j m-idx mdput ; 
: m-size ( addr -- n ) { addr } addr rows addr cols * ;

: m-init-shape ( addr m n -- ) { addr m n }
    addr dat-idx cells allot drop
    m addr row-idx put n addr col-idx put
    n addr rs-idx put 1 addr cs-idx put ( Assumes row contiguous ) ;
: m-init ( addr m n -- ) { addr m n }
    addr m n m-init-shape
    here addr dat-idx put addr mdata m n * cells allot drop ;
: m-add-data ( n1 n2 ... nN N addr --- ) { n addr } n 1 + 1 do addr n i - mdput loop ;
: m-create ( v1 v2 ... vN addr m n --- ) { addr m n }
    addr m n m-init m n * addr m-add-data ;

: m-copy ( addr1 addr2 -- ) { addr1 addr2 }
    addr2 addr1 rows addr1 cols m-init-shape
    addr1 mdata addr2 dat-idx put ;
    
variable .mvi
variable .mvj
\ Matrix view
: .mv ( addr -- ) { addr }
    cr .mvi 0! addr rows 0 do
        .mvj 0! addr cols 0 do
            addr .mvi @ .mvj @ m-val .n8
            .mvj ++
        loop cr .mvi ++
    loop ;
\ Matrix info 
: .mi ( addr -- ) { addr }
    cr ." Shape:  ( " addr rows . ." , " addr cols . ." )"
    cr ." Stride: ( " addr rstride . ." , " addr cstride . ." )" ;
\ Matrix info + matrix data
: .ma ( addr -- ) { addr } addr .mi addr .mv ;
    

\ Shape checking
: shape= ( addr1 addr2 -- n ) { addr1 addr2 }
    addr1 rows addr2 rows = addr1 cols addr2 cols = and ;
: shape/= ( addr1 addr2 -- n ) shape= invert ;
: shape-conform ( addr1 addr2 -- n ) { addr1 addr2 } addr1 cols addr2 rows = ;
: shape-/conform ( addr1 addr2 -- n ) shape-conform invert ;
: m= ( addr1 addr2 -- n ) { addr1 addr2 }
    addr1 addr2 shape=
    addr1 rows 0 do addr1 cols 0 do addr1 j i m-val addr2 j i m-val = and loop loop ;

\ Addition
: m+ ( addr1 addr2 addr3 -- addr3 ) { addr1 addr2 addr3 }
    addr1 addr2 shape/= if ." Shapes are not equal" exit then
    addr3 addr1 rows addr1 cols m-init
    addr3 m-size 0 do
        addr1 i mdget addr2 i mdget + addr3 i mdput
    loop addr3 ;

\ Dot product
variable m.i  \ i, j and k change depending on the position of the loop
variable m.j  \ see loop-test for details, so using my own indexes
variable m.k
: m. ( addr1 addr2 addr3 -- addr3 ) { addr1 addr2 addr3 }
    addr1 addr2 shape-/conform if ." Matrices do not conform" exit then
    addr3 addr1 rows addr2 cols m-init
    m.i 0! addr3 rows 1 + 0 do
        m.j 0! addr3 cols 1 + 0 do
            m.k 0! 0 addr1 cols 0 do
                addr1 m.i @ m.k @ m-val addr2 m.k @ m.j @ m-val * +
                m.k ++
            loop addr3 m.i @ m.j @ m-store
            m.j ++
        loop m.i ++
    loop addr3 ;

\ Transposition
: swap-vals ( addr idx1 idx2 -- ) { addr idx1 idx2 }
    addr idx1 get addr idx2 get addr idx1 put addr idx2 put ;
: m.t ( addr -- addr ) { addr }
    addr rs-idx cs-idx swap-vals addr row-idx col-idx swap-vals addr ;

\ *** tests *** /
: loop-test ( -- )
    3 0 do
        cr ." i" i .
        6 3 do
            cr ." i" i . ."  j" j . 
            9 6 do
                cr ." i" i . ."  j" j . ."  k" k .
            loop
        loop
    loop ;

create test-no 0 ,
: test-msg ( n -- )
    cr if test-no ? ." Passed." else test-no ? ." Failed." then 1 test-no +! ;
variable test-m1 4 5 6 40 50 60 test-m1 2 3 m-create
variable test-m2 7 8 9 70 80 90 test-m2 2 3 m-create
variable test-m3 4 5 40 50 400 500 test-m3 3 2 m-create
: test-shape= ( -- ) 1 test-no !
    test-m1 test-m2 shape= test-msg
    test-m1 test-m3 shape= invert test-msg ;
variable test-m1+m2 11 13 15 110 130 150 test-m1+m2 2 3 m-create
variable test-m1+m2-o
: test-m+ ( -- ) 3 test-no !
    test-m1 test-m2 test-m1+m2-o m+
    test-m1+m2-o test-m1+m2 m= test-msg ;

variable test-m1.m3 2616 3270 26160 32700 test-m1.m3 2 2 m-create
variable test-m1.m3-o
: test-m. ( -- ) 4 test-no !
    test-m1 test-m3 test-m1.m3-o m. test-m1.m3 m= test-msg ;

variable test-m1-copy
: test-m-copy ( -- ) 5 test-no !
    test-m1 test-m1-copy m-copy
    test-m1 test-m1-copy m= test-msg ;

: test-m.t ( -- ) 6 test-no !
    test-m1 test-m1-copy m-copy
    test-m1-copy m.t m.t test-m1 m= test-msg
    test-m1-copy m.t test-m3 shape= test-msg ;

: run-tests ( -- ) test-shape= test-m+ test-m. test-m-copy test-m.t ;
