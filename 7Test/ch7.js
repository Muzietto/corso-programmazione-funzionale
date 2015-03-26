function grade(student, mark) {
  'use strict';
  return function (w) {
    return w(student, mark);
  }   
}

function printGrade(grade) {
  'use strict';
  return grade(function(student, mark) {
    return student + ': ' + mark;
  });  
}

function printListOfGrade(listaOfGrade) {
  'use strict';
  return listaOfGrade.map(function(val) {
    return printGrade(val);
  }).join(', ');
}

function valuta(student, mark) {
  'use strict';
  if (mark < 18) {
    return grade(student, 'insufficiente');
  }
  if (mark <= 22) {
    return grade(student, 'sufficiente');
  }
  if (mark <= 27) {
    return grade(student, 'buono');
  }
  return grade(student, 'ottimo');
}

function fromMarkToGrade(mark) {
  return mark(valuta);
}

function valutaList(lista) {
  'use strict';
  var result = [];
  return helper(lista);
  function helper(lista) {
    if (lista.length === 0) {
      return result;
    }
    result.push(fromMarkToGrade(lista[0]));
    return helper(lista.slice(1, lista.length));
  }
}

function creaValList(names, grades) {
  'use strict';
  var minLength = Math.min(names.length, grades.length), result = [], i;
  for(i = 0; i < minLength; i = i + 1) {
    result.push(grade(names[i], grades[i]));
  }  
  return result;
}

function media(lista) {
  'use strict';
  var sommaVoti = 0, numeroVoti = 0;
  function sommaAndConta(lista) {
    if (lista.length > 0) {
      sommaVoti = sommaVoti + lista[0](function(student, mark) {
        return mark;
      });
      numeroVoti = numeroVoti + 1;
      sommaAndConta(lista.slice(1, lista.length));
    }
  }
  sommaAndConta(lista);
  return sommaVoti / numeroVoti;
}

function separa(lista) {
  'use strict';
  var result = {promossi: [], bocciati: []};
  lista.reduce(function(acc, val) {
    if(val(function(std, grade) {return grade;}) >= 18) {
      result.promossi.push(val);  
    } else {
      result.bocciati.push(val); 
    }
  }, result);
  result.promossi = printListOfGrade(result.promossi);
  result.bocciati = printListOfGrade(result.bocciati);
  return result;
}