var expect = chai.expect;

describe('chapter 7', function () {
  describe('before exercise A.1', function () {
    beforeEach(function(){
    });
    it('grade and  printGrade works as expected',function(){
      expect(printGrade(grade('Bianchi',  16))).to.be.equal('Bianchi: 16');
      expect(printGrade(grade('Rossi',  'ottimo'))).to.be.equal('Rossi: ottimo');
    });
  });
  describe('exercise A.1', function () {
    beforeEach(function(){
    });
    it('valuta works as expected',function(){
      expect(printGrade(valuta('Bianchi',  16))).to.be.equal('Bianchi: insufficiente');
      expect(printGrade(valuta('Rossi',  22))).to.be.equal('Rossi: sufficiente');
      expect(printGrade(valuta('Neri',  23))).to.be.equal('Neri: buono');
      expect(printGrade(valuta('Blu',  28))).to.be.equal('Blu: ottimo');
      expect(printGrade(valuta('Gialli',  27))).to.be.equal('Gialli: buono');
    });
  });
  describe('before exercise A.2', function () {
    beforeEach(function(){
      this.listOfGrade1 = [grade("Bianchi", 16), grade("Rossi" , 20), grade( "Verdi",  24 ), grade( "Neri" , 29)];
      this.listOfGrade2 = [grade("Bianchi", "insufficiente"), grade("Rossi", "sufficiente"), grade("Verdi", "buono"), grade("Neri", "ottimo")];
    });
    it('printListOfGrade works as expected',function(){
      expect(printListOfGrade(this.listOfGrade1)).to.be.equal('Bianchi: 16, Rossi: 20, Verdi: 24, Neri: 29');
      expect(printListOfGrade(this.listOfGrade2)).to.be.equal('Bianchi: insufficiente, Rossi: sufficiente, Verdi: buono, Neri: ottimo');
    });
    it('fromMarkToGrade works as expected',function(){
      expect(printGrade(fromMarkToGrade(grade('Bianchi', 16)))).to.be.equal('Bianchi: insufficiente');
      expect(printGrade(fromMarkToGrade(grade('Rossi', 20)))).to.be.equal('Rossi: sufficiente');
    });
  });
  describe('exercise A.2', function () {
    beforeEach(function(){
      this.listOfGrade1 = [grade("Bianchi", 16), grade("Rossi" , 20), grade( "Verdi",  24 ), grade( "Neri" , 29)];
    });
    it('valutaList works as expected',function(){
      expect(printListOfGrade(valutaList(this.listOfGrade1))).to.be.equal('Bianchi: insufficiente, Rossi: sufficiente, Verdi: buono, Neri: ottimo');
    });
  });
  describe('exercise A.3', function () {
    beforeEach(function(){
      this.st1 = ["Alpi", "Brambilla", "Ceri", "Dusi", "Elba", "Ferrari", "Gigli", "Ibis"];
      this.st2 = ["Verdi", "Rossi"];
      this.voti1 = [16, 24, 28, 18, 15, 23, 30, 28];
      this.voti2 =  [24, 18, 30, 28];
    });
    it('creaValList works as expected',function(){
      expect(printListOfGrade(creaValList(this.st1, this.voti1))).to.be.equal('Alpi: 16, Brambilla: 24, Ceri: 28, Dusi: 18, Elba: 15, Ferrari: 23, Gigli: 30, Ibis: 28');
      expect(printListOfGrade(creaValList(this.st1, this.voti2))).to.be.equal('Alpi: 24, Brambilla: 18, Ceri: 30, Dusi: 28');
      expect(printListOfGrade(creaValList(this.st2, this.voti1))).to.be.equal('Verdi: 16, Rossi: 24');
    });
  });
  describe('exercise A.4', function () {
    beforeEach(function(){
      this.st1 = ["Alpi", "Brambilla", "Ceri", "Dusi", "Elba", "Ferrari", "Gigli", "Ibis"];
      this.st2 = ["Verdi", "Rossi"];
      this.voti1 = [16, 24, 28, 18, 15, 23, 30, 28];
      this.voti2 =  [24, 18, 30, 28];
      this.valList1 = creaValList(this.st1, this.voti1);
      this.valList2 = creaValList(this.st1, this.voti2);
      this.valList3 = creaValList(this.st2, this.voti1);
    });
    it('media works as expected',function(){
      expect(media(this.valList1)).to.be.equal(22.75);
      expect(media(this.valList2)).to.be.equal(25.0);
      expect(media(this.valList3)).to.be.equal(20.0);
    });
  });
  describe('exercise A.5', function () {
    beforeEach(function(){
      this.st1 = ["Alpi", "Brambilla", "Ceri", "Dusi", "Elba", "Ferrari", "Gigli", "Ibis"];
      this.voti1 = [16, 24, 28, 18, 15, 23, 30, 28];
      this.valList1 = creaValList(this.st1, this.voti1);
    });
    it('separa works as expected',function(){
      expect(JSON.stringify(separa(this.valList1))).to.be.equal(JSON.stringify({promossi: 'Brambilla: 24, Ceri: 28, Dusi: 18, Ferrari: 23, Gigli: 30, Ibis: 28', bocciati: 'Alpi: 16, Elba: 15'}));
    });
  });
    describe('exercise B.1', function () {
    beforeEach(function(){
    });
    it('surname and category type works as expected',function(){
      expect(value(surname('Bianchi'))).to.be.equal('Bianchi');
      expect(value(surname('Rossi'))).to.be.equal('Rossi');
      expect(value(category('n'))).to.be.equal('Nursery');
      expect(value(category('d'))).to.be.equal('Daycare');
      expect(value(category('r'))).to.be.equal('Recreation');
      expect(stampChildDes(childDes(surname('Bianchi'), category('d')))).to.be.equal('Bianchi: Daycare');
      expect(stampChildDes(childDes(surname('Rossi'), category('n')))).to.be.equal('Rossi: Nursery');
      expect(stampChildDes(childDes(surname('Gialli'), category('r')))).to.be.equal('Gialli: Recreation');
    });
  });
    describe('exercise B.2', function () {
    beforeEach(function(){
      this.child1 = childDes(surname('Bianchi'), category('d'));
      this.child2 = childDes(surname('Rossi'), category('d'));
      this.child3 = childDes(surname('Gialli'), category('n'));
      this.child4 = childDes(surname('Marroni'), category('r'));
      this.child5 =  childDes(surname('Grigi'), category('r'));    
      this.childCollection = [this.child1, this.child2, this.child3, this.child4, this.child5];
    });
    it('number works as expected',function(){
      expect(number(category('d'), this.childCollection)).to.be.equal(2);
      expect(number(category('n'), this.childCollection)).to.be.equal(1);
      expect(number(category('r'), this.childCollection)).to.be.equal(2);
    });
  });
    describe('exercise B.3', function () {
    beforeEach(function(){
      this.child1 = childDes(surname('Bianchi'), category('d'));
      this.child2 = childDes(surname('Rossi'), category('d'));
      this.child3 = childDes(surname('Bianchi'), category('n'));
      this.child4 = childDes(surname('Rossi'), category('r'));
      this.child5 =  childDes(surname('Bianchi'), category('r'));    
      this.child6 = childDes(surname('Marroni'), category('r'));
      this.childCollection = [this.child1, this.child2, this.child3, this.child4, this.child5, this.child6];
    });
    it('pay works as expected',function(){
      expect(pay(surname('Bianchi'), this.childCollection)).to.be.equal(225+55+58);
      expect(pay(surname('Rossi'), this.childCollection)).to.be.equal(225+55);
      expect(pay(surname('Marroni'), this.childCollection)).to.be.equal(110);
    });
  });
});