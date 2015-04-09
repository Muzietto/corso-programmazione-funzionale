var expect = chai.expect;

describe('chapter 2',function(){
  describe('exercise 1',function(){
    beforeEach(function(){
    });
    it('costo return the right price with the discount applied',function(){
      expect(costo('cod1', prA, scA)).to.be.equal(18.0);
      expect(costo('cod1', prB, scA)).to.be.equal(36.0);
      expect(costo('cod2', prB, scB)).to.be.equal(75.375);
    });
  });
});