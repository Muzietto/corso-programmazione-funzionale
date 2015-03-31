function surname(student) {
	'use strict';
	return function (w) {
		return w(student);
	};
}

function category(abbr) {
	'use strict';
	if (abbr === 'd') {
		return function (w) {
			return w('Daycare');
		};
	}
	if (abbr === 'n') {
		return function (w) {
			return w('Nursery');
		};
	}
	if (abbr === 'r') {
		return function (w) {
			return w('Recreation');
		};
	}
}

function childDes(name, cat) {
	'use strict';
	return function (w) {
		return w(name, cat);
	};
}

function stampChildDes(childDes) {
	'use strict';
	return childDes(function (name, cat) {
		return value(name) + ': ' + value(cat);
	});
}

function number(category, childColl) {
	'use strict';
	if (childColl[0] === undefined) {
		return 0;
	}
	if (takeCat(childColl[0]) === value(category)) {
		return 1 + number(category, childColl.slice(1, childColl.length));
	}
	return number(category, childColl.slice(1, childColl.length));
}

function takeCat(childDes) {
	'use strict';
	return childDes(function (name, dest) {
		return value(dest);
	});
}

function pay(name, childColl) {
	'use strict';
	var sibiling = false;

	function helper(helperChildColl) {
		var money;
		if (helperChildColl[0] === undefined) {
			return 0;
		}
		if (takeSurname(helperChildColl[0]) === value(name)) {
			money = sibiling ? costPerCategory(helperChildColl[0]) / 2 : costPerCategory(helperChildColl[0]);
			sibiling = true;
			return money + helper(helperChildColl.slice(1, helperChildColl.length));
		}
		return helper(helperChildColl.slice(1, helperChildColl.length));
	}

	return helper(childColl);
}

function costPerCategory(childDes) {
  'use strict';
	var cat = takeCat(childDes);
	if (cat === 'Daycare') {
		return 225;
	}
	if (cat === 'Nursery') {
		return 116;
	}
	if (cat === 'Recreation') {
		return 110;
	}
}

function takeSurname(childDes) {
	'use strict';
	return childDes(function (name, dest) {
		return value(name);
	});
}
