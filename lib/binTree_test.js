var expect = chai.expect;

describe('BINARY TREE LIBRARY',function(){
  describe('testing the functionality of a pure functional binary tree library',function(){
    beforeEach(function(){
      this.intTree = binTree.insertFromList([1, 2, 3, 4, 5], binTree.empty());
      this.controlTree = binTree.insertFromList([1, 3, 4, 5], binTree.empty());
      this.evenTree = binTree.insertFromList([2, 4], binTree.empty());
      this.isEven = function(x) {
        return x % 2 === 0;
      }
    });
    it('remove work as expected',function(){
      expect(binTree.toString(binTree.remove(2, this.intTree))).to.be.equal(binTree.toString(this.controlTree));
    });
    it('remove work as expected',function(){
      expect(binTree.toString(binTree.filter(this.isEven, this.intTree))).to.be.equal(binTree.toString(this.evenTree));
    });
  });
});

