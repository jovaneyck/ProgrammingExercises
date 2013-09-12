package test

class Traits extends TestBase{
  behavior of "traits"
  
  it should "act as an interface that can have an implementation" in {
    trait Adder {
      def add(n : Int) = n + 1
    }
    
    object WithAdder extends Adder
    
    WithAdder.add(4) should be(5)
  }
  
  it should "support multiple inheritance" in {
    trait A{
      def a = "a"
    }
    
    trait B {
     def b = "b"
    }
    
    object Child extends A with B //note the with keyword!
    
    Child.a should be("a")
    Child.b should be("b")
  }
  
  it should "have no problems with the 'diamond problem'" in {
    trait Parent {
      def foo = "Parent"
    }
    
    trait FirstChild extends Parent {
      override def foo = "FirstChild" //note the override keyword!
    }
    
    trait SecondChild extends Parent {
      override def foo = "SecondChild" 
    }
    
    object GrandChild extends FirstChild with SecondChild
    
    GrandChild.foo should be("SecondChild") //which foo is called?
  }
  
}