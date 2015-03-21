var expect = chai.expect;

describe('in chapter 16',function(){
  describe('apt functions in namespace Seq handling stateless sequences as thunks',function(){
    beforeEach(function(){
      this.integers = Seq.sequence(function(x){ return x + 1; },0);
    });
    it('should build infinite sequences from functions',function(){
      expect(first(this.integers())).to.be.equal(0);
      expect(first(second(this.integers())())).to.be.equal(1);
      try {
        expect(this.integers('whatever')).to.fail;
      } catch (err) {
        expect(err).to.be.equal('invalid operation');
      }
    });
    it('should extract single elements from sequences by giving their position',function(){
      expect(Seq.nth(2,this.integers)).to.be.equal(2);
      expect(Seq.nth(222,this.integers)).to.be.equal(222);
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
      var from4on = Seq.skip(4,this.integers);
      expect(first(from4on())).to.be.equal(4);
      expect(Seq.nth(222,from4on)).to.be.equal(226);
    });
    it('should allow to create sub-sequences',function(){
      var first44 = Seq.take(44,this.integers);
      expect(first(first44())).to.be.equal(0);
      expect(Seq.nth(2,this.integers)).to.be.equal(2);
      try {
        expect(Seq.nth(222,first44)).to.fail;
      } catch (err) {
        expect(err).to.be.equal('empty sequence');
      }
    });
    it('should allow to combine skip and take',function(){
      var fortyToEighty = Seq.take(40,Seq.skip(40,this.integers));
      expect(first(fortyToEighty())).to.be.equal(40);
      expect(Seq.nth(2,fortyToEighty)).to.be.equal(42);
      try {
        expect(Seq.nth(222,fortyToEighty)).to.fail;
      } catch (err) {
        expect(err).to.be.equal('empty sequence');
      }
    });
  });
  describe('we define mapping for sequences such that',function(){
    it('')
  });
});

