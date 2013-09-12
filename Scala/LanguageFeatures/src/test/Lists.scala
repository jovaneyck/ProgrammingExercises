package test

class Lists extends TestBase{
  behavior of "lists" 
  
  it should "have an empty list concept" in{
    Nil.length should be(0)
  }
  
  it should "have a cons concept" in {
    ("an element" :: Nil).length should be(1)
  }
  
  it should "have a friendly notation" in {
    List(1,2).length should be(2)
  }
  
  it should "support the map function" in {
    List(1,2) map (_+1) should be(List(2,3)) //note the _ wildcard in lambda's!
  }
  
  it should "support the flatMap function" in {
    val result : List[Int] = List(1,3) flatMap (el => List(el, el+1))
    //map that returns lists -> flatMap compacts them all together
    result should be(List(1,2,3,4))
  }
  
  it should "support filtering" in {
    List(1,2,3) filter (_ != 2) should be(List(1,3))
  } 
  
  it should "support a fold" in {
    //fold requires a "zero-element"
    List(1,2,3).foldLeft("The list contains ")(_+_) should be("The list contains 123")
  }
  
  it should "support a reduce" in {
    //reduce takes the first element as "zero"
    List(1,2,3).reduceLeft(_+_) should be(6)
  }
  
  it should "be easy to combine lists" in {
    val left = List(1,2)
    val right = List('a','b')
    
    left zip right should be(List((1,'a'),(2,'b')))
  }
  
  it should "support an easy for all check" in {
    List(2,4).forall (_ % 2 == 0) should be(true)
  }
  
  it should "support easy grouping" in {
    List("a","aa","b","bb").groupBy(_ length) should be(Map(1 -> List("a", "b"), 2 -> List("aa", "bb")))
  }
  
  it should "support easy sorting" in {
    List(4,1,3,2).sorted should be (List(1,2,3,4))
  }
  
  it should "support arbitrary sorting" in {
	  List(4,1,3,2).sortWith(_>_) should be (List(4,3,2,1))
  }
  
  it should "allow easy updates" in {
    List(1,2,3) updated (2, 4) should be(List(1,2,4))
  } 
}