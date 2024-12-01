(
Day 1 of the challenge:

*** Part 1 ***
- Read an input of integers [ int1 int2 CR ] and split into two vectors.
- Sort the two vectors.
- Get the absolute difference between the vectors.
- Sum the results.


*** Part 2 ***
- For each element in vector 1, count how many times that number appears in vector 2.
- Multiply that element by the number of times it appears in vector 2.
- Sum the results.
)

\ Loads libraries
include utils.fth
include sort.fth
include math.fth
include matrix.fth

\ Loads the data onto the stack
include inputs/1.dat

\ Input data shape
2 constant n
depth n / constant m

\ Puts data into matrices
variable mat mat m n m-create

\ Sorts the arrays
mat m-sort

\ Gets the sum of the absolute difference between the two columns
: sum-abs-diff ( addr n1 n2 -- n ) { addr c1 c2 }
    0 addr rows 0 do
        addr i c1 m-val addr i c2 m-val - abs +
    loop ;
mat 0 1 sum-abs-diff .


: count-times-in ( addr n1 n2 -- n ) { addr num col }
    0 addr rows 0 do
        addr i col m-val num = if 1 + then
    loop ;

variable num
: simularity-score ( addr n1 n2 -- n ) { addr c1 c2 }
    0 addr rows 0 do
        addr i c1 m-val num !
        addr num @ c2 count-times-in num @ * +
    loop ;
mat 0 1 simularity-score .
