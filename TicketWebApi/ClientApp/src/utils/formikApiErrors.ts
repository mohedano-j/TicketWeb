export const formikTouchedFrom = (apiError?: any| null) => {
  const formikTouched: { [key: string]: boolean } = {};
  if (apiError !== null && apiError?.errors) {
    for (const key of Object.keys(apiError?.errors)) {
      formikTouched[toCamelCase(key)] = apiError?.errors[key] !== undefined;
    }
  }
  return formikTouched;
};

export const formikErrorsFrom = (apiError?: any | null) => {
  const formikErrors: { [key: string]: string } = {};
  if (apiError?.errors) {
    for (const key of Object.keys(apiError?.errors)) {
      formikErrors[toCamelCase(key)] = apiError?.errors[key].join("<br/>");
    }
  }
  return formikErrors;
};

const toCamelCase = (str: string) => str.replace(/(?:^\w|[A-Z]|\b\w)/g, (ltr, idx) => (idx === 0 ? ltr.toLowerCase() : ltr.toUpperCase())).replace(/\s+/g, "");
