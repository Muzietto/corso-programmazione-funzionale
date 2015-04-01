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
      var multiplyer = function(acc,curr){ return acc * curr; };
      expect(foldl([1,2,3,4],multiplyer,1)).to.be.equal(24);
      expect(mulWithFoldl([1,2,3,4])).to.be.equal(24);
      expect([1,2,3,4].reduce(multiplyer,1)).to.be.equal(24);
    });
    it('can count the elements of a given array',function(){
      var counter = function(acc,curr){ return acc + 1; };
      expect(foldl([1,2,3,4],counter,0)).to.be.equal(4);
      expect(countWithFoldl([1,2,3,4])).to.be.equal(4);
      expect([1,2,3,4].reduce(counter,0)).to.be.equal(4);
    });
    it('can find the min/max element of a given array',function(){
      var minEl = function(acc,curr){ 
        if (acc < curr) {
          return acc;
        }
        return curr;
      };
      expect(foldl([1,2,3,4],minEl, Math.pow(2,32) - 1)).to.be.equal(1);
      expect(minWithFoldl([1,2,3,4])).to.be.equal(1);
      expect([1,2,3,4].reduce(minEl, Math.pow(2,32) - 1)).to.be.equal(1);
      
      var maxEl = function(acc,curr){
        if (acc > curr) {
          return acc;
        }
        return curr;
      };
      expect(foldl([1,2,3,4],maxEl, -Math.pow(2,32) - 1)).to.be.equal(4);
      expect(maxWithFoldl([1,2,3,4])).to.be.equal(4);
      expect([1,2,3,4].reduce(maxEl, -Math.pow(2,32) - 1)).to.be.equal(4);
    });
    it('can easily reverse a given array',function(){
      var reverser = function(acc,curr){ 
        acc.unshift(curr);
        return acc;
      };
      expect(foldl([1,2,3,4],reverser,[])).to.be.eql([4, 3, 2, 1]);
      expect(revWithFoldl([1,2,3,4])).to.be.eql([4, 3, 2, 1]);
      expect([1,2,3,4].reduce(reverser,[])).to.be.eql([4, 3, 2, 1]);
    });
  });
  describe('fold RIGHT over arrays (aka fold)',function(){
    it('can sum all elements of a given array',function(){
      var adder = function(head,tail){ return head + tail; };
      expect(fold([1,2,3,4],adder,0)).to.be.equal(10);
      expect(sumWithFold([1,2,3,4])).to.be.equal(10);
      expect([1,2,3,4].reduceRight(adder,0)).to.be.equal(10);
    });
    it('can easily reverse a given array',function(){
      var reverser = function(acc, curr){ 
        acc.push(curr);
        return acc;
      };
      expect(fold([1,2,3,4],reverser,[])).to.be.eql([4, 3, 2, 1]);
      expect(revWithFold([1,2,3,4])).to.be.eql([4, 3, 2, 1]);
      expect([1,2,3,4].reduceRight(reverser,[])).to.be.eql([4, 3, 2, 1]);
    });
    it('can multiply all elements of a given array',function(){
      var multiplyer = function(acc,curr){ return acc * curr; };
      expect(fold([1,2,3,4],multiplyer,1)).to.be.equal(24);
      expect(mulWithFold([1,2,3,4])).to.be.equal(24);
      expect([1,2,3,4].reduceRight(multiplyer,1)).to.be.eql(24);
    });
    it('can count the elements of a given array',function(){
      var counter = function(acc,curr){ return acc + 1; };
      expect(fold([1,2,3,4],counter,0)).to.be.equal(4);
      expect(countWithFold([1,2,3,4])).to.be.equal(4);
      expect([1,2,3,4].reduceRight(counter,0)).to.be.eql(4);
    });
    it('can find the min/max element of a given array',function(){
      var minEl = function(acc,curr){ 
        if (acc < curr) {
          return acc;
        }
        return curr;
      };
      expect(fold([1,2,3,4],minEl, Math.pow(2,32) - 1)).to.be.equal(1);
      expect(minWithFold([1,2,3,4])).to.be.equal(1);
      expect([1,2,3,4].reduceRight(minEl, Math.pow(2,32) - 1)).to.be.equal(1);
      
      var maxEl = function(acc,curr){
        if (acc > curr) {
          return acc;
        }
        return curr;
      };
      expect(foldl([1,2,3,4],maxEl, -Math.pow(2,32) - 1)).to.be.equal(4);
      expect(maxWithFold([1,2,3,4])).to.be.equal(4);
      expect([1,2,3,4].reduceRight(maxEl, -Math.pow(2,32) - 1)).to.be.equal(4);
    });
  });
});

