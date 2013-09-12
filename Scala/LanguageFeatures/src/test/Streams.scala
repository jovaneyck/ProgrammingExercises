package test

class Streams extends TestBase{
  behavior of "streams" 
  
  it should "represent an infinite list" in{
    (Stream.from(1) take 3).toList should be(List(1,2,3))
  }
  
  it should "be possible to create an infinite stream with complex values" in {
    def evens(from : Int) : Stream[Int] = from #:: evens(from+2) 
    		//inifinite recursion, but the #:: operator for streams applies lazy evaluation of the tail
    
    ((evens(2) take 3) toList) should be(List(2,4,6))
  }
}