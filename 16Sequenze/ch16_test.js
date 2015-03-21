var expect = chai.expect;

describe('in chapter 16',function(){
  describe('apt functions in namespace Seq handling stateless sequences as thunks',function(){
    beforeEach(function(){
      this.naturals = Seq.sequence(function(x){ return x + 1; },0);
      this.undefineds = Seq.sequence(function(){ /*return undefined;*/ },0);
    });
    it('should build infinite sequences from functions',function(){
      expect(first(this.naturals())).to.be.equal(0);
      expect(first(second(this.naturals())())).to.be.equal(1);
      try {
        expect(this.naturals('whatever')).to.fail;
      } catch (err) {
        expect(err).to.be.equal('invalid operation');
      }
      try {
        expect(this.undefineds()).to.fail;
      } catch (err) {
        expect(err).to.be.equal('empty sequence');
      }
    });
    it('should extract single elements from sequences by giving their position',function(){
      expect(Seq.nth(2,this.naturals)).to.be.equal(2);
      expect(Seq.nth(222,this.naturals)).to.be.equal(222);
    });
    it('should build finite sequences from arrays',function(){
      var nineToFive = Seq.sequence([9,8,7,6,5]);
      expect(first(nineToFive())).to.be.equal(9);
      expect(first(second(nineToFive())())).to.be.equal(8);
      expect(Seq.nth(2,nineToFive)).to.be.equal(7);
      try {
        expect(Seq.nth(6,nineToFive)).to.fail;
      } catch (err) {
        expect(err).to.be.equal('empty sequence');
      }
    });
    it('should allow to skip sequence elements',function(){
      var from4on = Seq.skip(4,this.naturals);
      expect(first(from4on())).to.be.equal(4);
      expect(Seq.nth(222,from4on)).to.be.equal(226);
    });
    it('should allow to create sub-sequences',function(){
      var first44 = Seq.take(44,this.naturals);
      expect(first(first44())).to.be.equal(0);
      expect(Seq.nth(2,this.naturals)).to.be.equal(2);
      try {
        expect(Seq.nth(222,first44)).to.fail;
      } catch (err) {
        expect(err).to.be.equal('empty sequence');
      }
    });
    it('should allow to combine skip and take',function(){
      var fortyToEighty = Seq.take(40,Seq.skip(40,this.naturals));
      expect(first(fortyToEighty())).to.be.equal(40);
      expect(Seq.nth(2,fortyToEighty)).to.be.equal(42);
      try {
        expect(Seq.nth(222,fortyToEighty)).to.fail;
      } catch (err) {
        expect(err).to.be.equal('empty sequence');
      }
    });
  });
  describe('various apt operations on sequences',function(){
    beforeEach(function(){
      this.naturals = Seq.sequence(function(x){ return x + 1; },0);
      this.first10 = Seq.take(10,this.naturals);
      this.square = function(x){ return x * x; };
      this.divisibleBy3 = function(x){ return x % 3 === 0; };
    });
    it.skip('should allow to map sequences over a function',function(){
      var squares = Seq.map(this.square,this.naturals);
      expect(Seq.nth(2,squares)).to.be.equal(4);
      expect(Seq.nth(15,squares)).to.be.equal(361);

      var first10squares = Seq.map(this.square,this.first10);
      expect(Seq.nth(2,first10squares)).to.be.equal(4);
      try {
        expect(Seq.nth(15,first10squares)).to.fail;
      } catch (err) {
        expect(err).to.be.equal('empty sequence');
      }
    });
    it.skip('should allow to filter sequences using a predicate',function(){
      var multiplesOf3 = Seq.filter(this.divisibleBy3,this.naturals);
      expect(Seq.nth(2,multiplesOf3)).to.be.equal(6);
      expect(Seq.nth(20,multiplesOf3)).to.be.equal(57);

      var first10multiplesOf3 = Seq.map(this.divisibleBy3,this.first10);
      expect(Seq.nth(2,first10multiplesOf3)).to.be.equal(6);
      try {
        expect(Seq.nth(20,first10multiplesOf3)).to.fail;
      } catch (err) {
        expect(err).to.be.equal('empty sequence');
      }
    });
    it.skip('should allow to generate Fibonacci numbers',function(){
    });
    it.skip('should allow to generate prime numbers using a sieve of Eratosthenes',function(){
    });
  });
});

