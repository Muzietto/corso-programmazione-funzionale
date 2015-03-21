var expect = chai.expect;

describe('chapter 6',function(){
  describe('before exercise A.1',function(){
    beforeEach(function(){
    });
    it('we verify that binary trees work fine',function(){
      expect(empty()(label)).to.be.null;
      expect(empty()(left)).to.be.null;
      expect(empty()(right)).to.be.null;
      expect(printTree(empty())).to.be.equal('empty()');

      var node3 = node(3,empty(),empty());
      expect(printTree(node3)).to.be.equal('node(3,empty(),empty())');
      expect(node3(label)).to.be.equal(3);
      expect(isEmptyLeaf(node3(left))).to.be.true;
      expect(node3(left)(label)).to.be.null;
      expect(isEmptyLeaf(node3(right))).to.be.true;
      expect(isEmptyLeaf(node3)).to.be.false;
      
      var nodes23 = node(2,empty(),node(3,empty(),empty()));
      expect(printTree(nodes23)).to.be.equal('node(2,empty(),node(3,empty(),empty()))');
      expect(nodes23(right)(label)).to.be.equal(3);
      expect(isEmptyLeaf(nodes23(right)(left))).to.be.true;
      expect(isEmptyLeaf(nodes23(right))).to.be.false;      
    });
  });
  describe('exercise A.1',function(){
    it.skip('insert works are we wish',function(){
      var nodes4 = node(4,empty(),empty());
      var nodes34 = insert(3,node4);
      expect(printTree(nodes34)).to.be.equal('node(3,empty(),node(4,empty(),empty()))');
      
      var nodes1234 = insert(2,insert(1,nodes34));
      expect(printTree(nodes1234)).to.be.equal('result_here');
      expect(nodes1234(right)(left)(label)).to.be.equal('result_here');
    })
  });
  describe('exercise A.2',function(){
    beforeEach(function(){
      this.intList = [ 20, 10, 60, 15, 40, 100, 30, 50, 70, 35, 42, 58, 75, 32, 37 ];
      this.strList1 = [ "pesca", "banana", "uva", "albicocca", "nocciola", "ribes" ];
      this.strList2 = [ "limone", "ciliegia", "mela", "pera", "noce"  ];      
    });
    it.skip('insertFromList produces intTree',function(){
      
    });
    it.skip('insertFromList produces strTree1',function(){
      
    })
    it.skip('insertFromList produces strTree2',function(){
      
    })
  });
  describe('exercise A.3',function(){
    it.skip('intToFloatTree works are we wish',function(){
    })
  });
});

