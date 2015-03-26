var expect = chai.expect;

describe('chapter 7',function(){
  describe('before exercise A.1',function(){
    beforeEach(function(){
    });
    it('grade and  printGrade works as expected',function(){
      expect(printGrade(grade('Bianchi',  16))).to.be.equal('Bianchi: 16');
      expect(printGrade(grade('Rossi',  'ottimo'))).to.be.equal('Rossi: ottimo');
    });
  });
  describe('exercise A.1',function(){
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
  describe('before exercise A.2',function(){
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
  describe('exercise A.2',function(){
    beforeEach(function(){
      this.listOfGrade1 = [grade("Bianchi", 16), grade("Rossi" , 20), grade( "Verdi",  24 ), grade( "Neri" , 29)];
    });
    it('valutaList works as expected',function(){
      expect(printListOfGrade(valutaList(this.listOfGrade1))).to.be.equal('Bianchi: insufficiente, Rossi: sufficiente, Verdi: buono, Neri: ottimo');
    });
  });
  describe('exercise A.3',function(){
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
  describe('exercise A.4',function(){
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
  describe('exercise A.5',function(){
    beforeEach(function(){
      this.st1 = ["Alpi", "Brambilla", "Ceri", "Dusi", "Elba", "Ferrari", "Gigli", "Ibis"];
      this.voti1 = [16, 24, 28, 18, 15, 23, 30, 28];
      this.valList1 = creaValList(this.st1, this.voti1);
    });
    it('separa works as expected',function(){
      expect(JSON.stringify(separa(this.valList1))).to.be.equal(JSON.stringify({promossi: 'Brambilla: 24, Ceri: 28, Dusi: 18, Ferrari: 23, Gigli: 30, Ibis: 28', bocciati: 'Alpi: 16, Elba: 15'}));
    });
  });
});