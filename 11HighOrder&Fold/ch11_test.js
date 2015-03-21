var expect = chai.expect;

describe('in chapter 11',function(){
  describe('fold LEFT over arrays (aka foldl)',function(){
    it('can sum all elements of a given array',function(){
      var adder = function(acc,curr){ return acc + curr; };
      expect(foldl([1,2,3,4],adder,0)).to.be.equal(10);
      expect(sumWithFoldl([1,2,3,4])).to.be.equal(10);
      expect([1,2,3,4].reduce(adder,0)).to.be.equal(10);
    });
    it('can multiply all elements of a given array',function(){
    });
    it('can count the elements of a given array',function(){
    });
    it('can find the min/max element of a given array',function(){
    });
    it('can easily reverse a given array',function(){
    });
  });
  describe('fold RIGHT over arrays (aka fold)',function(){
    it.skip('can sum all elements of a given array',function(){
      var adder = function(head,tail){ return head + tail; };
      expect(fold([1,2,3,4],adder,0)).to.be.equal(10);
      expect(sumWithFold([1,2,3,4])).to.be.equal(10);
      expect([1,2,3,4].reduceRight(adder,0)).to.be.equal(10);
    });
    it('can multiply all elements of a given array',function(){
    });
    it('can count the elements of a given array',function(){
    });
    it('can find the min/max element of a given array',function(){
    });
  });
});

