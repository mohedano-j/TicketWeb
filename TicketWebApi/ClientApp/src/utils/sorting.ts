export const sortBy = (key: any, sortOrder: string, list: any[], lookup?: Record<string, string>, transform?: (value: any) => string) => {
  if (list.length <= 1) return list; // No sorting if lenght 0 or 1
  let arrayCopy = list;
  if (arrayCopy.length) arrayCopy.sort(compareBy(key, sortOrder, list, lookup, transform));
  return arrayCopy;
};

function compareBy(key: string | number, sortOrder: string, list: { [x: string]: any }[], lookup?: Record<string, string>, transform?: (value: any) => string) {
  //TODO This method can be refactored to reduced number of lines
  // previous sort order for column switched on the fly
  if (sortOrder == "desc") {
    return function (a: any, b: any) {
      let valueA = a[key];
      let valueB = b[key];
      if (lookup) {
        valueA = lookup[a[key]];
        valueB = lookup[b[key]];
      }

      if (transform) {
        valueA = transform(valueA);
        valueB = transform(valueB);
      }

      if (valueA === undefined || valueB === null) return 1;
      if (valueA === undefined || valueB === null) return -1;
      if (typeof valueA === "number") {
        return valueA - valueB;
      } else {
        if (a[key][0] === "$") {
          let amountA = valueA;
          let amountB = valueB;

          amountA = parseInt(amountA.replace(/[$,.]/g, ""));
          amountB = parseInt(amountB.replace(/[$,.]/g, ""));

          return amountA - amountB;
        }

        if (valueA < valueB) return -1;
        if (valueA > valueB) return 1;
      }

      return 0;
    };
  } else if (sortOrder == "asc") {
    return function (a: any, b: any) {
      let valueA = a[key];
      let valueB = b[key];
      if (lookup) {
        valueA = lookup[a[key]];
        valueB = lookup[b[key]];
      }

      if (transform) {
        valueA = transform(valueA);
        valueB = transform(valueB);
      }
      if (valueA === undefined || valueA === null) return 1;
      if (valueB === undefined || valueB === null) return -1;
      if (typeof valueA === "number") {
        return valueB - valueA;
      } else {
        if (a[key][0] === "$") {
          let amountA = valueA;
          let amountB = valueB;

          amountA = parseInt(amountA.replace(/[$,.]/g, ""));
          amountB = parseInt(amountB.replace(/[$,.]/g, ""));

          return amountB - amountA;
        }

        if (valueA < valueB) return 1;
        if (valueA > valueB) return -1;
      }
      return 0;
    };
  }
}
