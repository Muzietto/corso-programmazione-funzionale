var expect = chai.expect;

describe("chapter 4", function() {
  describe('exercise 1',function(){
    beforeEach(function(){
    });
    it('remove even numbers from a list',function(){
      expect(rmEven([2, 5, 5, 87, 6, 100, 2])).to.deep.equal([5, 5, 87]);
    });
  }); 
  describe('exercise 2',function(){
    beforeEach(function(){
    });
    it('remove all elements in odd position from a list, considering the first element an even position.',function(){
      expect(rmOddPos([20.4, 21.4, 22.4, 23.4, 24.4, 25.4, 26.4, 27.4])).to.deep.equal([20.4, 22.4, 24.4, 26.4]);
    });
  }); 
  describe('exercise 3',function(){
    beforeEach(function(){
    });
    it('generate a list of all int from input to 0 via s steps',function(){
      expect(down(10, 3)).to.deep.equal([10, 7, 4, 1]	);
      expect(down(10, -29)).to.deep.equal([10]);
    });
  }); 
  describe('exercise 4',function(){
    beforeEach(function(){
    });
    it('generate a list of all int  0 to n',function(){
      expect(upto(8)).to.deep.equal([0, 1, 2, 3, 4, 5, 6, 7, 8]);
    });
  }); 
});
