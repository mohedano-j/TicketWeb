import React from "react";

type Props = {
  name: string;
  instance: any;
};

export const ObjectTable = ({ name, instance }: Props) => {
  if (!instance) return <div>{name} is null or undefined</div>;
  return (
    <table className="table table-bordered table-condensed table-strpied table-sm">
      <thead>
        <tr>
          <th style={{ textAlign: "center" }} colSpan={2}>
            {name} object
          </th>
        </tr>
      </thead>
      <tbody>
        {Object.entries(instance).map(([k, v], i) => {
          const propertyKey = k;

          let propertyValue: any = v;
          if (typeof v === "object") propertyValue = "Object";
          if (Array.isArray(v)) propertyValue = "Array(" + v.length + ")";

          return (
            <tr key={propertyKey}>
              <td style={{ width: "50%" }}>{propertyKey}</td>
              <td style={{ width: "50%" }}>{propertyValue}</td>
            </tr>
          );
        })}
      </tbody>
    </table>
  );
};
