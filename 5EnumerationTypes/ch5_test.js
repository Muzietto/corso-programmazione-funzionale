var expect = chai.expect;

describe('in ch5',function(){
  describe('shapes are defined',function(){
    beforeEach(function(){
    });
    it('with options as areas',function(){
      expect(area(rectangle(2,4))).to.be.equal(8);
      expect(area(square(4))).to.be.equal(16);
      expect(area(triangle(2,4))).to.be.equal(4);
    });
  });
});

