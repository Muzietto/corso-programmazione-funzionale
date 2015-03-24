var expect = chai.expect;

describe('chapter 5',function(){
  describe('exercise 1',function(){
    beforeEach(function(){
    });
    it('shapes are functions and their areas are options',function(){
      expect(value(area(rectangle(2,4)))).to.be.equal(8);
      expect(isSome(area(rectangle(2,4)))).to.be.true;
      expect(value(area(square(4)))).to.be.equal(16);
      expect(value(area(triangle(2,4)))).to.be.equal(4);
      
      expect(value(areaSum(rectangle(2,4),triangle(2,4),square(4)))).to.be.equal(28);

      expect(value(area(rectangle(2,-4)))).to.be.equal(null);
      expect(isSome(area(rectangle(2,-4)))).to.be.false;
      expect(value(areaSum(rectangle(2,-4),triangle(2,4),square(4)))).to.be.equal(20);
    });
  });
  describe('exercise 2',function(){
    beforeEach(function(){
    });
    it('head_tailOpt returns couples (option(head),option(tail)); options are nones when list is empty and somes otherwise',function(){
      expect(isNone(first(head_tailOpt([])))).to.be.true;
      expect(isNone(second(head_tailOpt(list())))).to.be.true;

      expect(isSome(first(head_tailOpt([1])))).to.be.true;
      expect(value(first(head_tailOpt(list(1))))).to.be.equal(1);
      expect(isNone(second(head_tailOpt([1])))).to.be.true;

      expect(isSome(first(head_tailOpt([1,'due',3])))).to.be.true;
      expect(value(first(head_tailOpt(list(1,'due',3))))).to.be.equal(1);
      expect(isSome(second(head_tailOpt(list(1,'due',3))))).to.be.true;
      expect(head(value(second(head_tailOpt([1,'due',3]))))).to.be.equal('due');
    });
    it('lastOpt returns none when list is empty and some otherwise',function(){
      expect(isNone(lastOpt([]))).to.be.true;
      expect(isNone(lastOpt(list()))).to.be.true;

      expect(isSome(lastOpt([1]))).to.be.true;
      expect(value(lastOpt(list(1)))).to.be.equal(1);

      expect(isSome(lastOpt([1,'due',3]))).to.be.true;
      expect(value(lastOpt(list(1,'due',3)))).to.be.equal(3);
    });
    it('catOpt removes nones from list and unwraps somes',function(){
      expect(isEmpty(catOpt([none()]))).to.be.true;
      expect(isEmpty(catOpt(list(none())))).to.be.true;

      expect(isEmpty(catOpt([some(1),none()]))).to.be.false;
      expect(catOpt(list(none(),none(),some(1)))[0]).to.be.equal(1);
    });
    it('mynth returns somes for existing list elems and none otherwise',function(){
      expect(isSome(mynth([1],0))).to.be.true;
      expect(value(mynth(list(1),0))).to.be.equal(1);

      expect(isNone(mynth([],0))).to.be.true;
      expect(isNone(mynth(list(),0))).to.be.true;

      expect(isNone(mynth([100],100))).to.be.true;
      expect(isNone(mynth(list(100),100))).to.be.true;
    });
  });
  describe('exercise 3',function(){
    beforeEach(function(){
    });
    it('printVal print the description of the tagged value given in input',function(){
      expect(printVal(Int(0))).to.be.equal("0 : int;");
      expect(printVal(Bool(true))).equal("true : bool;");
      expect(printVal(Int(44-66))).to.be.equal("-22 : int;");
      expect(printVal(Bool( 10 === 5 + 5 ))).to.be.equal("true : bool;");
    });
    it('printTl print a description of the tagged list given in input',function(){
      expect(printTl([Int(0)])).to.be.equal("0 : int;");
      expect(printTl([Int(0), Int(1), Bool(true), Int(4), Bool(false)])).to.be.equal("0 : int;1 : int;true : bool;4 : int;false : bool;");
      expect(printTl([Int(3), Bool(4>6), Int(44-66), Bool( 10 === 5 + 5 )])).to.be.equal("3 : int;false : bool;-22 : int;true : bool;");
    });
  });
});

