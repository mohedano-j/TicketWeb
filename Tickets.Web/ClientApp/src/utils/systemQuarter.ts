export const SystemQuarter = (quarterString: string | undefined | null) => {
  const quarter = quarterString?.trim() ?? "";

  const yearString = quarter.length == 5 ? quarter.substring(0, 4) : null;

  const quarterNumberString = quarter.length == 5 ? quarter.substring(4, 5) : null;

  const isValid = () => quarter.match(/^\d{4}[1234]$/) != null;

  const toPrettyName = () => {
    if (isValid()) {
      let quarterText = "";
      switch (quarterNumberString) {
        case "1":
          quarterText = "Winter";
          break;
        case "2":
          quarterText = "Spring";
          break;
        case "3":
          quarterText = "Summer";
          break;
        case "4":
          quarterText = "Fall";
          break;
      }
      return [quarterText, yearString].join(" ");
    }
    return "-";
  };

  const toShortName = () => {
    if (isValid()) {
      let quarterText = "";
      switch (quarterNumberString) {
        case "1":
          quarterText = "W";
          break;
        case "2":
          quarterText = "S";
          break;
        case "3":
          quarterText = "M";
          break;
        case "4":
          quarterText = "F";
          break;
      }
      return [quarterText, yearString?.substring(2, 4)].join("");
    }
    return "-";
  };

  const year = () => year;

  const quarterNumber = () => quarterNumber;

  return {
    quarter,
    toPrettyName,
    toShortName,
    quarterNumber,
    year,
    isValid,
  };
};
