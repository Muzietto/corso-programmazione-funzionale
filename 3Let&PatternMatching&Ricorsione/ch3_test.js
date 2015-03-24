var expect = chai.expect;

describe('chapter 3',function(){
  describe('exercise 1',function(){
    beforeEach(function(){
    });
    it('simplify has to simplify the fraction using the MCD function',function(){
      expect(simplify(15,9)).to.deep.equal([5,3]);
      expect(simplify(7,5)).to.deep.equal([7,5]);
    });
  });
  describe('exercise 2',function(){
    beforeEach(function(){
    });
    it('copy has to replicate the string passed as first parameter ',function(){
      expect(copy("Ciao", 4)).to.be.equal("CiaoCiaoCiaoCiao");
    });
  });
  describe('exercise 3',function(){
    beforeEach(function(){
    });
    it('sum1 has to sum all the number from 0 to te input number, sum2 sum all the number from the first input to the second input',function(){
      expect(sum1(4)).to.be.equal(10);
      expect(sum2(2,5)).to.be.equal(14);
    });
  });
  describe('exercise 4',function(){
    beforeEach(function(){
    });
    it('costo return the right price with the discount applied',function(){
      expect(fib(11)).to.be.equal(89);
      expect(fib(12)).to.be.equal(144);
      expect(fib(20)).to.be.equal(6765);
    });
  });  
});