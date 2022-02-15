export const SystemDateTime = (dateString: string | undefined) => {
  let year: string, month: string, day: string;

  const isValid = () => (dateString && dateString.length == 8 && dateString.match(/^\d{4}(0[1-9]|1[0-2])(0[1-9]|[12]\d|3[01])$/) ? Date.parse(dateString) != NaN : false);

  if (dateString?.length == 8 && isValid) {
    year = dateString.substring(0, 4);
    month = dateString.substring(4, 6);
    day = dateString.substring(6, 8);
  }

  const toPrettyName = () => {
    if (isValid()) {
      return [month, day, year].join("/");
    }
    return "-";
  };

  const toYyyyMmDd = () => (isValid() ? [year, month, day].join("") : null);

  return {
    toPrettyName,
    toYyyyMmDd,
    isValid,
  };
};
